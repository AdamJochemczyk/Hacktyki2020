﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(CreateUserDto createUserDto)
        {
            if (createUserDto == null) return BadRequest("User is null");
            var user = await _authorizationService.RegistrationUserAsync(createUserDto);
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
        {
            if (!await _authorizationService.SignIn(userLoginDto))
            {
                return BadRequest("Failed Login");
            }
            return Ok(userLoginDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> SetPassword(int id,UpdateUserPasswordDto updateUserDto)
        {
            if (id != updateUserDto.UserId) return BadRequest("Users isn't the same"); 
            var user_set = await _authorizationService.SetPassword(updateUserDto);
            return Ok(user_set);
        }
    }
}