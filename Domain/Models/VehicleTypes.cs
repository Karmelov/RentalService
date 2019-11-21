using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalAPI.Domain.Models
{
    public enum Price { Premium=150, Basic=100 }

    public class VehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Price DailyCost { get; set; }
        public int DiscountAfterDays { get; set; }
        public float DiscountedPrice { get; set; }
        public float PointsPerRental { get; set; }
    }
}
