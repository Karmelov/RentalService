using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalAPI.Domain.Models
{ 
    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime StartDate { get; set; }
        public int DaysPaid { get; set; }
        public DateTime ReturnDate { get; set; }
        public float ExtraFees;
    }
}
