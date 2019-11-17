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

namespace RentalAPI.Infrastructure.Repositories.VolatileRepository
{
    public class VolatileVehicleRepository : IVehicleRepository
    {
        Dictionary<int, Vehicle> Instances;

        public VolatileVehicleRepository()
        {
            Instances = new Dictionary<int, Vehicle>();
            Vehicle testVehicle = new Vehicle
            {
                Id = 1,
                LicensePlate = "AB1234",
                CurrentlyRented = false
            };

            Instances[testVehicle.Id] = testVehicle;
        }
        public async Task<IEnumerable<Vehicle>> ListAllAsync()
        {
            return Instances.Values.OrderBy(x => x.Id).ToList();
        }
        public async Task<Vehicle> GetById(int id)
        {
            if (!Instances.ContainsKey(id))
            {
                return null;
            }
            return Instances[id];
        }
    }
}
