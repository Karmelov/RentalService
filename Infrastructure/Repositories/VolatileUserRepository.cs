using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RentalAPI.Domain.Models;
using RentalAPI.Domain.Repositories;
using RentalAPI.Infrastructure;
using RentalAPI.Domain.Services;

namespace RentalAPI.Infrastructure.Repositories.VolatileRepository
{
    public class VolatileUserRepository : IUserRepository
    {
        public async Task<IEnumerable<User>> ListAllAsync()
        {
            Dictionary<int, User> dict = FakeDB.UsersTable();
            return dict.Values.OrderBy(x => x.Id).ToList();
        }

        public async Task<User> GetById(int id)
        {
            Dictionary<int, User> dict = FakeDB.UsersTable();
            if (!dict.ContainsKey(id))
            {
                return null;
            }
            return dict[id];
        }

        public async Task<User> GetByName(string name) {
            Dictionary<int, User> dict = FakeDB.UsersTable();

            User result = dict.Where(x => x.Value.Name == name).FirstOrDefault().Value;

            return result;
        }

        public async Task<UserRepositoryResponse> UpdateUser(User user) {
            Dictionary<int, User> dict = FakeDB.UsersTable();
            try
            {
                dict[user.Id] = user;
            }
            catch (Exception e)
            {
                return new UserRepositoryResponse(e.Message);
            }
            UserRepositoryResponse response = new UserRepositoryResponse(user);
            return response;
        }
    }
}
