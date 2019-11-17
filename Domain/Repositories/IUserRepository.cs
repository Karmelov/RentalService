using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;

namespace RentalAPI.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> ListAllAsync();
        public Task<User> GetById(int id);
    }
}
