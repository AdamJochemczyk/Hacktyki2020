using CarRental.API.Attributes;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _usersService.GetAllUsers();
            if (result == null) return NotFound("Database users is empty");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetUserAsync(int id)
        {
 
            var user = await _usersService.GetUser(id);
            if (user == null) return NotFound("This ID does not exist");
            if (id == 0) return BadRequest("This ID does not exist");
             user = await _usersService.GetUser(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {

            if (!await _usersService.DeleteUser(id))
                return NotFound("This ID does not exist");
            return Ok();
        }

        [HttpPatch("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> UpdateUserAsync(UsersDto usersDto)
        { 
            var result = await _usersService.UpdateUser(usersDto);
            if (result.isValid == false)
                return NotFound("User Not Found");
            return Ok(result);
        }
    }
}