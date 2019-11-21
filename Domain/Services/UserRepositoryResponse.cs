using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;

namespace RentalAPI.Domain.Services
{
    public class UserRepositoryResponse : BaseResponse
    {
        public User User { get; private set; }

        private UserRepositoryResponse(bool success, string message, User user) : base(success, message)
        {
            User = user;
        }

        public UserRepositoryResponse(User user) : this(true, string.Empty, user)
        { }

        public UserRepositoryResponse(string message) : this(false, message, null)
        { }
    }
}
