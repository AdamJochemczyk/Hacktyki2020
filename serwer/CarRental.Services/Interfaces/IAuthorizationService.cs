﻿using CarRental.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
   public interface IAuthorizationService
    {
        Task<CreateUserDto> RegistrationUserAsync(CreateUserDto createUserDto);
        Task<bool> SetPassword(UpdateUserPasswordDto updateUser);
        Task<string> SignIn(UserLoginDto userLogin);
    }
}
