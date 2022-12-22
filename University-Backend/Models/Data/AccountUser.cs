using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Backend.Models.Data
{
    public class AccountUser
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string? Password { get; set; }
    }
}