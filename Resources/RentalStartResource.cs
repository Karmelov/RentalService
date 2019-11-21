using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalAPI.Resources
{
    public class RentalStartResource
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLincesePlate { get; set; }
        public DateTime StartDate { get; set; }
        public float InitialPayment { get; set; }
    }
}
