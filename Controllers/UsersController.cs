using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalAPI.Domain.Repositories;
using RentalAPI.Domain.Models;

namespace RentalAPI.Controllers
{
    [Route("/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllAsync() {
            var users = await _userRepository.ListAllAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                Response.StatusCode = 404;
            }
            return user;
        }

    }
}
