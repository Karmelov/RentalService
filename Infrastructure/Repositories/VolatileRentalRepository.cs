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
        public async Task<IEnumerable<Rental>> ListAllAsync()
        {
            Dictionary<int, Rental> dict = FakeDB.RentalsTable();
            List<Rental> result = dict.Values.OrderBy(x => x.Id).ToList();
            return result;
        }

        public async Task<Rental> GetById(int id) {
            Dictionary<int, Rental> dict = FakeDB.RentalsTable();
            if (!dict.ContainsKey(id))
            {
                return null;
            }
            return dict[id];
        }

        public async Task<RentalRepositoryResponse> SaveAsync(Rental rental) {
            Dictionary<int, Rental> dict = FakeDB.RentalsTable();
            try
            {
                rental.Id = dict.Count + 1;
                dict[rental.Id] = rental;
            }
            catch (Exception e)
            {
                return new RentalRepositoryResponse(e.Message);
            }
            RentalRepositoryResponse response = new RentalRepositoryResponse(rental);
            return response;
        }

        public async Task<RentalRepositoryResponse> UpdateRental(Rental rental) {
            Dictionary<int, Rental> dict = FakeDB.RentalsTable();
            try
            {
                dict[rental.Id] = rental;
            }
            catch (Exception e)
            {
                return new RentalRepositoryResponse(e.Message);
            }
            return new RentalRepositoryResponse(rental);

        }

        public async Task<RentalRepositoryResponse> DeleteRental(int id) {
            Dictionary<int, Rental> dict = FakeDB.RentalsTable();
            try
            {
                dict.Remove(id);
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
