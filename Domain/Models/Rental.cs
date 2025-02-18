﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalAPI.Domain.Models
{ 
    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public int DaysRented { get; set; }
        public float InitialPayment { get; set; }
        public bool Returned { get; set; }
        public DateTime ReturnDate { get; set; }
        public float ExtraFees { get; set; }
    }
}
