using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RentalAPI.Domain.Models;

namespace RentalAPI.Domain.Repositories
{
    public interface IVehicleRepository
    {
        public Task<IEnumerable<Vehicle>> ListAllAsync();

        public Task<Vehicle> GetById(int id);
    }
}
