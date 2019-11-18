using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalAPI.Domain.Repositories;
using RentalAPI.Domain.Models;
using AutoMapper;
using RentalAPI.Resources;

namespace RentalAPI.Controllers
{
    [Route("/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetAllAsync() {
            var users = await _userRepository.ListAllAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<UserResource> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                Response.StatusCode = 404;
            }
            var resource = _mapper.Map<User, UserResource>(user);
            return resource;
        }

    }
}
