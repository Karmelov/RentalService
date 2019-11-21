using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RentalAPI.Domain.Models;
using RentalAPI.Domain.Repositories;
using RentalAPI.Domain.Services;

namespace RentalAPI.Infrastructure.Repositories
{
    public class VolatileVehicleTypeRepository : IVehicleTypeRepository
    {
        public async Task<IEnumerable<VehicleType>> ListAllAsync()
        {
            Dictionary<int, VehicleType> dict = FakeDB.VehicleTypesTable();
            return dict.Values.OrderBy(x => x.Id).ToList();
        }

        public async Task<VehicleType> GetById(int id)
        {
            Dictionary<int, VehicleType> dict = FakeDB.VehicleTypesTable();
            if (!dict.ContainsKey(id))
            {
                return null;
            }
            return dict[id];
        }
    }
}
