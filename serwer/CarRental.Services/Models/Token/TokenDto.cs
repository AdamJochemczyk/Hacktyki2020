using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.Token
{
   public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }   
        public int ErrorCode { get; set; }
    }
}
