using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalAPI.Domain.Repositories;
using RentalAPI.Domain.Models;

namespace RentalAPI.Controllers
{
    [Route("/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            var vehicles = await _vehicleRepository.ListAllAsync();
            return vehicles;
        }
        [HttpGet("{id}")]
        public async Task<Vehicle> GetById(int id) {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                Response.StatusCode = 404;
            }
            return vehicle;
        }
    }
}
