using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RentalAPI.Domain.Models;
using RentalAPI.Domain.Repositories;

namespace RentalAPI.Infrastructure.Repositories.VolatileRepository
{
    public class VolatileUserRepository : IUserRepository
    {
        Dictionary<int,User> Instances;

        public VolatileUserRepository() {
            Instances = new Dictionary<int, User>();
            User testUser = new User{
                Id = 1,
                Name = "Chillax",
                Password = "1234"
            };
            Instances[testUser.Id] = testUser;
        }
        public async Task<IEnumerable<User>> ListAllAsync() {
            return Instances.Values.OrderBy(x => x.Id).ToList();
        }

        public async Task<User> GetById(int id)
        {
            if (!Instances.ContainsKey(id))
            {
                return null;
            }
            return Instances[id];
        }
    }
}
