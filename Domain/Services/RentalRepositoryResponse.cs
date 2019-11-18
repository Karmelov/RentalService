using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;

namespace RentalAPI.Domain.Services
{
    public class RentalRepositoryResponse : BaseResponse
    {
        public Rental Rental { get; private set; }

        private RentalRepositoryResponse(bool success, string message, Rental rental) : base(success, message)
        {
            Rental = rental;
        }

        public RentalRepositoryResponse(Rental rental) : this(true, string.Empty, rental)
        { }

        public RentalRepositoryResponse(string message) : this(false, message, null)
        { }
    }
}
