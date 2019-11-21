using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RentalAPI.Resources
{
    public class UserLoginResource
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
