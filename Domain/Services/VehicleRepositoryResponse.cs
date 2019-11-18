using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;

namespace RentalAPI.Domain.Services
{
    public class VehicleRepositoryResponse : BaseResponse
    {
        public Vehicle Vehicle { get; private set; }

        private VehicleRepositoryResponse(bool success, string message, Vehicle vehicle) : base(success, message)
        {
            Vehicle = vehicle;
        }

        public VehicleRepositoryResponse(Vehicle vehicle) : this(true, string.Empty, vehicle)
        { }

        public VehicleRepositoryResponse(string message) : this(false, message, null)
        { }
    }
}
