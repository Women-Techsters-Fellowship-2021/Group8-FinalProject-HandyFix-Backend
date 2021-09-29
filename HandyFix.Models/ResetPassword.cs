using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyFix.Models
{
   public class ResetPassword
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string OTP { get; set; }
        //public DateTime InsertDateTimeUTC { get; set; }

        //Navigation properties
        public User User { get; set; }

    }
}