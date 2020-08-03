using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.User
{
   public class UsersDto:BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumberIdentificate { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
}
