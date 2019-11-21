using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;

namespace RentalAPI.Domain.Repositories
{
    public interface IVehicleTypeRepository
    {
        public Task<IEnumerable<VehicleType>> ListAllAsync();
        public Task<VehicleType> GetById(int id);
    }
}

