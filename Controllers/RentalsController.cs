using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalAPI.Domain.Repositories;
using RentalAPI.Domain.Models;
using RentalAPI.Resources;
using RentalAPI.Extensions;
using AutoMapper;
using RentalAPI.Domain.Services;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace RentalAPI.Controllers
{
    [Route("/[controller]")]
    public class RentalsController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        private readonly IMapper _mapper;

        public RentalsController(IRentalRepository rentalRepository, IVehicleRepository vehicleRepository, IUserRepository userRepository, IVehicleTypeRepository vehicleTypeRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            var rentals = await _rentalRepository.ListAllAsync();
            return rentals;
        }

        [HttpGet("{id}")]
        public async Task<Rental> GetById(int id)
        {
            var rental = await _rentalRepository.GetById(id);
            if (rental == null)
            {
                Response.StatusCode = 404;
            }
            return rental;
        }

        [HttpPost]
        public async Task<IActionResult> StartRental([FromBody] CreateRentalResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            try
            {
                ValidateToken(resource.UserId.Value, resource.Token);
            }
            catch (Exception e) {
                return Unauthorized(e.Message);
            }

            Vehicle availableVehicle = await _vehicleRepository.GetAvailableVehicleAsync(resource.VehicleType.Value);
            if (availableVehicle == null)
            {
                return NotFound("No vehicles available");
            }

            User client = await _userRepository.GetById(resource.UserId.Value);
            if (client == null)
            {
                return NotFound("User doesn't exist");
            }

            Rental rental = await FillRentalData(resource, availableVehicle);

            RentalRepositoryResponse result = await _rentalRepository.SaveAsync(rental);
            if (!result.Success)
                return BadRequest(result.Message);

            SetVehicleAsRented(availableVehicle);

            RentalStartResource rentalResource = _mapper.Map<Rental, RentalStartResource>(result.Rental);
            rentalResource.UserName = client.Name;
            rentalResource.VehicleLincesePlate = availableVehicle.LicensePlate;

            AddPointsToClient(client, availableVehicle);
            
            return Ok(rentalResource);
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnAsync(int id, [FromBody] ReturnRentalResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            Rental rental = await _rentalRepository.GetById(id);
            if (rental == null) {
                return NotFound("Rental not found");
            }

            try
            {
                ValidateToken(rental.UserId, resource.Token);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }

            Vehicle vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId);

            vehicle.CurrentlyRented = false;
            await _vehicleRepository.UpdateVehicle(vehicle);

            rental.ReturnDate = resource.ReturnDate.Value.Date;
            rental.Returned = true;

            int daysSinceRentalStart = Convert.ToInt32((rental.ReturnDate - rental.StartDate).TotalDays);

            int daysOverRentalTime = daysSinceRentalStart - rental.DaysRented;
            if (daysOverRentalTime > 0)
            {
                rental.ExtraFees = await CalculateOverCharge(vehicle, daysOverRentalTime);
            }

            await _rentalRepository.UpdateRental(rental);

            return Ok(rental);
        }

        private async Task<Rental> FillRentalData(CreateRentalResource resource, Vehicle vehicle)
        {
            int initialPayment = await CalculateRentalCost(vehicle, resource.Days.Value);

            Rental rental = _mapper.Map<CreateRentalResource, Rental>(resource);
            rental.VehicleId = vehicle.Id;
            rental.UserId = resource.UserId.Value;
            rental.DaysRented = resource.Days.Value;
            rental.Returned = false;
            rental.InitialPayment = initialPayment;
            rental.StartDate = resource.Date.Value.Date;

            return rental;
        }

        void SetVehicleAsRented(Vehicle vehicle)
        {
            vehicle.CurrentlyRented = true;
            _vehicleRepository.UpdateVehicle(vehicle);
        }

        private async Task<int> CalculateRentalCost(Vehicle vehicle, int days)
        {
            VehicleType type = await _vehicleTypeRepository.GetById(vehicle.VehicleTypeId);
            if (days <= type.DiscountAfterDays)
            {
                return days * (int)type.DailyCost;
            }
            else
            {
                int cost = type.DiscountAfterDays * (int)type.DailyCost;

                int discountedDays = days - type.DiscountAfterDays;
                cost += (int)((float)discountedDays * (float)type.DailyCost * type.DiscountedPrice);
                return cost;
            }
        }

        private async Task<float> CalculateOverCharge(Vehicle vehicle, int days)
        {
            VehicleType type = await _vehicleTypeRepository.GetById(vehicle.VehicleTypeId);
            return (float)days * (float)type.DailyCost;
        }

        private JwtSecurityToken DecryptToken(string tokenString)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.ReadJwtToken(tokenString);
                return token;
            }
            catch (Exception e) {
                return null;
            }
        }

        private void ValidateToken(int userId, string token) {
            JwtSecurityToken tokenObject = DecryptToken(token);

            if (tokenObject == null)
            {
                throw new Exception("Invalid token");
            }

            Int64 tokenUserId = (Int64)tokenObject.Payload["UserId"];
            if (tokenUserId != userId)
            {
                throw new Exception("Unauthorized");
            }

            DateTime expirationTime = Convert.ToDateTime(tokenObject.Payload["ExpirationDate"]);
            if (expirationTime < DateTime.Now)
            {
                throw new Exception("Token Expired");
            }
        }

        private async void AddPointsToClient(User client, Vehicle vehicle) {
            VehicleType type = await _vehicleTypeRepository.GetById(vehicle.VehicleTypeId);
            client.Points += type.PointsPerRental;
            await _userRepository.UpdateUser(client);
        }
    }
}