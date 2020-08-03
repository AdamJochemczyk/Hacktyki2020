using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUserDto createUserDto)
        {
            if (createUserDto == null) return BadRequest("User is null");
            var user = await _authorizationService.RegistrationUserAsync(createUserDto);
            if (user.UserId == 0)
                return BadRequest("This Email already exists");
            return Ok(user);
        }
        //
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
        {
            var cos = await _authorizationService.SignIn(userLoginDto);
            if (cos.ErrorCode == 401)
                return Unauthorized("Email/Password not correct");
            return Ok(cos);
        }
        [HttpPut]
        public async Task<IActionResult> SetPassword(UpdateUserPasswordDto updateUserPassword)
        {
            if (!await _authorizationService.SetPassword(updateUserPassword))
                return BadRequest("Password isn't the same please check");        
            return Ok();
        }
    }
}