using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using CarRental.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _usersService.GetAllUsers();
            if (result == null) return BadRequest("Database users is empty");
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync(int Id)
        {
                if (Id == 0) return BadRequest("This ID does not exist");
            var user = await _usersService.GetUser(Id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(int Id)
        {
            if (Id == 0) return BadRequest("This ID does not exist");
            await _usersService.DeleteUser(Id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int Id,UsersDto usersDto)
        {
            if (usersDto.UserId != Id||usersDto.UserId==0) return BadRequest("This user does not exist");
            var result = await _usersService.UpdateUser(usersDto);
            return Ok(result);
        }
    }
}