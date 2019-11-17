using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;

namespace RentalAPI.Domain.Repositories
{
    public interface IRentalRepository
    {
        public Task<IEnumerable<Rental>> ListAllAsync();

        public Task<Rental> GetById(int id);
    }
}
