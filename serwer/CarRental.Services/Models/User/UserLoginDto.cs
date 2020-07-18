using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.User
{
   public class UserLoginDto
    {
        public string Email { get; set; }
        public string EncodePassword { get; set; }
    }
}
