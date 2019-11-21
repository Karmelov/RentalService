using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;
using RentalAPI.Domain.Services;

namespace RentalAPI.Domain.Repositories
{
    public interface IVehicleRepository
    {
        public Task<IEnumerable<Vehicle>> ListAllAsync();
        public Task<Vehicle> GetByIdAsync(int id);
        public Task<Vehicle> GetAvailableVehicleAsync(int vehicleType);
        public Task<VehicleRepositoryResponse> UpdateVehicle(Vehicle vehicle);

    }
}
