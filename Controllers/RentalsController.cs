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

namespace RentalAPI.Controllers
{
    [Route("/[controller]")]
    public class RentalsController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public RentalsController(IRentalRepository rentalRepository, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
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
        public async Task<IActionResult> PostAsync([FromBody] CreateRentalResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            Vehicle availableVehicle = await _vehicleRepository.GetAvailableVehicleAsync();

            if (availableVehicle == null) {
                return NotFound("No vehicles available");
            }

            Rental rental = FillRentalData(resource, availableVehicle);
            RentalRepositoryResponse result = await _rentalRepository.SaveAsync(rental);

            if (!result.Success)
                return BadRequest(result.Message);

            availableVehicle.CurrentlyRented = true;
            await _vehicleRepository.UpdateVehicle(availableVehicle);

            RentalStartResource rentalResource = _mapper.Map<Rental, RentalStartResource>(result.Rental);
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
            Vehicle vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId);

            vehicle.CurrentlyRented = false;
            await _vehicleRepository.UpdateVehicle(vehicle);

            rental.ReturnDate = resource.ReturnDate.Value.Date;
            rental.Returned = true;

            int daysSinceRentalStart = Convert.ToInt32((rental.ReturnDate - rental.StartDate).TotalDays);

            if (daysSinceRentalStart > rental.DaysRented) {
                rental.ExtraFees = daysSinceRentalStart * 150;
            }

            await _rentalRepository.UpdateRental(rental);

            return Ok(rental);
        }

        private Rental FillRentalData(CreateRentalResource resource, Vehicle vehicle) {
            Rental rental = _mapper.Map<CreateRentalResource, Rental>(resource);
            rental.VehicleId = vehicle.Id;
            rental.UserId = resource.UserId.Value;
            rental.DaysRented = resource.Days.Value;
            rental.Returned = false;
            rental.InitialPayment = rental.DaysRented * 100;
            rental.StartDate = resource.Date.Value.Date;

            return rental;
        }


    }
}