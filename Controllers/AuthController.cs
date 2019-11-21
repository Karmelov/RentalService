using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalAPI.Domain.Repositories;
using RentalAPI.Domain.Models;
using AutoMapper;
using RentalAPI.Resources;
using RentalAPI.Extensions;
using System.IdentityModel;  
using System.Security;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace RentalAPI.Controllers
{

    [Route("/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserLoginResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            string username = resource.Name;
            string md5password = Helpers.CreateMD5(resource.Password);

            User client = await _userRepository.GetByName(username);
            if (client == null) {
                return NotFound("User not found");
            }

            if (client.Password != md5password) {
                return Unauthorized("Incorrect credentials");
            }

            string token = GenerateToken(client);

            AuthTokenResource tokenResponse = new AuthTokenResource();
            tokenResponse.Token = token;
            return Ok(tokenResponse);
        }

        string GenerateToken(User client)
        {
            // The keys should be handled outside of the code, through environment variables for example.
            string key = "401b09fab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954557b7a0812e1081c39b740293f765eae731f5af5ed1";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new JwtHeader(credentials);
            var payload = new JwtPayload
            {
               { "UserId", client.Id},
               { "ExpirationDate", DateTime.Now.AddHours(1) }
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }

    }
}

