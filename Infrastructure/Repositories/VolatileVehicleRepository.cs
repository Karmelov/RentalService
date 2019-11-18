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

namespace RentalAPI.Infrastructure.Repositories.VolatileRepository
{
    public class VolatileVehicleRepository : IVehicleRepository
    {
        static Dictionary<int, Vehicle> Instances;

        public VolatileVehicleRepository()
        {
            if (Instances == null)
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
        }
        public async Task<IEnumerable<Vehicle>> ListAllAsync()
        {
            return Instances.Values.OrderBy(x => x.Id).ToList();
        }
        public async Task<Vehicle> GetByIdAsync(int id)
        {
            if (!Instances.ContainsKey(id))
            {
                return null;
            }
            return Instances[id];
        }

        public async Task<Vehicle> GetAvailableVehicleAsync() {
            var result = Instances.Where(x => x.Value.CurrentlyRented == false).FirstOrDefault().Value;
            return result;
        }
        public async Task<VehicleRepositoryResponse> UpdateVehicle(Vehicle vehicle)
        {
            try
            {

                Instances[vehicle.Id] = vehicle;
            }
            catch (Exception e)
            {
                return new VehicleRepositoryResponse(e.Message);
            }
            VehicleRepositoryResponse response = new VehicleRepositoryResponse(vehicle);
            return response;
        }
    }
}
