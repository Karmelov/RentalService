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
    public class VehiclesTypesController : Controller
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        public VehiclesTypesController(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleType>> GetAllAsync()
        {
            var vehicleTypes = await _vehicleTypeRepository.ListAllAsync();
            return vehicleTypes;
        }
        [HttpGet("{id}")]
        public async Task<VehicleType> GetById(int id)
        {
            var vehicleType = await _vehicleTypeRepository.GetById(id);
            if (vehicleType == null)
            {
                Response.StatusCode = 404;
            }
            return vehicleType;
        }
    }
}
