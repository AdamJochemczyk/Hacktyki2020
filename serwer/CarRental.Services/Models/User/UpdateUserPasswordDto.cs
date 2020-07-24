﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CarRental.Services.Models.User
{
   public class UpdateUserPasswordDto
    {
       // public int UserId { get; set; }
        public string EncodePassword { get; set; }
        public string ConfirmEncodePassword { get; set; }
        public JwtSecurityToken Token { get; }
        
    }
}
