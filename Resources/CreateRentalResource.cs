using System;
using System.ComponentModel.DataAnnotations;

namespace RentalAPI.Resources
{
    public class CreateRentalResource
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int? UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? Days { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public int? VehicleType { get; set; }
    }
}
