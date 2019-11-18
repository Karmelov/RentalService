using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;
using RentalAPI.Domain.Services;
using RentalAPI.Resources;


namespace RentalAPI.Domain.Repositories
{
    public interface IRentalRepository
    {
        public Task<IEnumerable<Rental>> ListAllAsync();

        public Task<Rental> GetById(int id);

        public Task<RentalRepositoryResponse> SaveAsync(Rental rental);
        public Task<RentalRepositoryResponse> UpdateRental(Rental rental);
        public Task<RentalRepositoryResponse> DeleteRental(int id);
    }

}
