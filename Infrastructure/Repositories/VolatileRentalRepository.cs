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
using RentalAPI.Resources;

namespace RentalAPI.Infrastructure.Repositories
{
    public class VolatileRentalRepository : IRentalRepository
    {
        static Dictionary<int, Rental> Instances;
        static int Index;

        public VolatileRentalRepository()
        {
            if (Instances == null)
            {
                Instances = new Dictionary<int, Rental>();
                Index = 1;
            }
        }

        public async Task<IEnumerable<Rental>> ListAllAsync()
        {
            List<Rental> result = Instances.Values.OrderBy(x => x.Id).ToList();
            return result;
        }

        public async Task<Rental> GetById(int id) {
            if (!Instances.ContainsKey(id))
            {
                return null;
            }
            return Instances[id];
        }

        public async Task<RentalRepositoryResponse> SaveAsync(Rental rental) {
            try
            {
                rental.Id = Index;
                Instances[rental.Id] = rental;
                Index++;
            }
            catch (Exception e)
            {
                return new RentalRepositoryResponse(e.Message);
            }
            RentalRepositoryResponse response = new RentalRepositoryResponse(rental);
            return response;
        }

        public async Task<RentalRepositoryResponse> UpdateRental(Rental rental) {
            try
            {
                Instances[rental.Id] = rental;
            }
            catch (Exception e)
            {
                return new RentalRepositoryResponse(e.Message);
            }
            return new RentalRepositoryResponse(rental);

        }

        public async Task<RentalRepositoryResponse> DeleteRental(int id) {
            try
            {
                Instances.Remove(id);
            }
            catch (Exception e)
            {
                return new RentalRepositoryResponse(e.Message);
            }
            Rental none = null;
            return new RentalRepositoryResponse(none);
        }
    }
}
