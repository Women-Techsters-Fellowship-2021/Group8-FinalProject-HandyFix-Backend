using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HandyFix.Models
{
    public class RegisterationRequest
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
    }
}
