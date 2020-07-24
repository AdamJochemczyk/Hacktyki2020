using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.User
{
   public class UpdateUserPasswordDto
    {
        public string EncodePassword { get; set; }
        public string ConfirmEncodePassword { get; set; }
        public string Token { get; set; }
    }
}
