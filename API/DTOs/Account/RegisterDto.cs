using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
