
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace HandyFix.Models
{
    public class UserEmailRequest {
        [Required]
        [EmailAddress]
        public string Email {get; set; }
    }
}