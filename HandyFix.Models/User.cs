
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandyFix.Models
{
    public class User : IdentityUser
    {
        //public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
