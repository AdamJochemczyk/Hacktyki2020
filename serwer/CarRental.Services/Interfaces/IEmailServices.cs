using CarRental.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Interfaces
{
    public interface IEmailServices
    {
        public bool EmailAfterRegistration(CreateUserDto createUserDto);
    }
}
