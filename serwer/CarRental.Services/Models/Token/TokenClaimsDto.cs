using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.Token
{
    public class TokenClaimsDto
    {
        public bool CheckRefreshToken { get; set; }
        public int Id { get; set; }
        public int UserId {get;set;}
    }
}
