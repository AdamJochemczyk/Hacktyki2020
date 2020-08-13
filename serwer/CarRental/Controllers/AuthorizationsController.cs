using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationsController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        public AuthorizationsController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUserDto createUserDto)
        {
            if (createUserDto == null) return NotFound("User is null");
            var user = await authorizationService.RegistrationUserAsync(createUserDto);
            if (user.UserId == 0)
                return BadRequest("This Email already exists");
            return Ok(user);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
        {
            var signInResult = await authorizationService.SignIn(userLoginDto);
            if (signInResult.Code == (int)HttpStatusCode.Unauthorized)
                return Unauthorized("Email/Password not correct");
            return Ok(signInResult);
        }

        [HttpPut]
        public async Task<IActionResult> SetPassword(UpdateUserPasswordDto updateUserPassword)
        {
            if (!await authorizationService.SetPassword(updateUserPassword))
                return NotFound("Code of Verification is bad");
            return Ok();
        }
    }
}