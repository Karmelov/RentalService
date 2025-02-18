﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RentalAPI.Resources
{
    public class ReturnRentalResource
    {
        [Required]
        public DateTime? ReturnDate { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
