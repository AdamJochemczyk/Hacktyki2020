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
            if (user.UserId == 0)
                return BadRequest("This Email already exists");
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
        {
            var cos = await _authorizationService.SignIn(userLoginDto);
            //{
                //return BadRequest("Failed Login");
           // }
            return Ok(cos);
        }
        [HttpPut]
        public async Task<IActionResult> SetPassword(string token)
        {
            var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI0NCIsInN1YiI6IkJvaGRhbiIsImVtYWlsIjoiYm9nZGFuLmt1Y2hlcjA5QGdtYWlsLmNvbSIsImV4cCI6MTU5NTU5Njg0NCwiaXNzIjoiTXlBdXRoU2VydmVyIiwiYXVkIjoiTXlBdXRoQ2xpZW50In0.vy6dFi-B2-4uCT7aXVU_fLZRlNVMQJxD501qRBuItUc";
            var handler = new JwtSecurityTokenHandler();
            var tokenn = handler.ReadJwtToken(jwt);
            if (!await _authorizationService.SetPassword(tokenn))
                return BadRequest("Password isn't the same please check");
            return Ok(tokenn);
        }
    }
}