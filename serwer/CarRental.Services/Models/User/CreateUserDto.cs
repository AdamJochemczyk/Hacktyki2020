﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.User
{
    public class CreateUserDto
    {
        //Maybe i don't need this
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumberIdentificate { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string EncdodePassword { get; set; }
        public int RoleOfWorker { get; set; }
    }
}
