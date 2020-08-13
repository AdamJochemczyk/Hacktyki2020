using System.Resources;
using System.Threading.Tasks;
using CarRental.API.Resources;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationsController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        public  ResourceManager resourcesManager;
        public AuthorizationsController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }

        /// <summary>
        /// Register user 
        /// Check if input email exciting in database
        /// If yes then return BadRequest
        /// else return Ok(User)
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return NotFound(resourcesManager.GetString("UserNull"));
            }
            var user = await authorizationService.RegistrationUserAsync(createUserDto);
            if (user == null)
            {
                return BadRequest(resourcesManager.GetString("EmailExiciting"));
            }
            return Ok(user);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignInAsync(UserLoginDto userLoginDto)
        {
            var cos = await authorizationService.SignInAsync(userLoginDto);
            if (cos.Code == 401)
            {
                return Unauthorized(resourcesManager.GetString("EmailPassword"));

            }
            return Ok(cos);
        }

        [HttpPut]
        public async Task<IActionResult> SetPasswordAsync(UpdateUserPasswordDto updateUserPassword)
        {
            if (!await authorizationService.SetPasswordAsync(updateUserPassword))
            {
                return NotFound(resourcesManager.GetString("CodeVerification"));
            }
            return Ok();
        }
    }
}