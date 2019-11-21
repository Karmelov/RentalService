using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RentalAPI.Domain.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public bool CurrentlyRented { get; set; }
        public int VehicleTypeId { get; set; }
    }
}
