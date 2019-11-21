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
        public async Task<IEnumerable<Vehicle>> ListAllAsync()
        {
            Dictionary<int, Vehicle> dict = FakeDB.VehiclesTable();
            return dict.Values.OrderBy(x => x.Id).ToList();
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            Dictionary<int, Vehicle> dict = FakeDB.VehiclesTable();
            if (!dict.ContainsKey(id))
            {
                return null;
            }
            return dict[id];
        }

        public async Task<Vehicle> GetAvailableVehicleAsync(int vehicleType) {
            Dictionary<int, Vehicle> dict = FakeDB.VehiclesTable();
            var result = dict.Where(x => x.Value.CurrentlyRented == false && x.Value.VehicleTypeId == vehicleType).FirstOrDefault().Value;
            return result;
        }

        public async Task<VehicleRepositoryResponse> UpdateVehicle(Vehicle vehicle)
        {
            Dictionary<int, Vehicle> dict = FakeDB.VehiclesTable();
            try
            {
                dict[vehicle.Id] = vehicle;
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
