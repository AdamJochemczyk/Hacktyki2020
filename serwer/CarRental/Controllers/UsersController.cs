using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using CarRental.Services.Services;
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

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = await _usersService.CreateUserAsync(createUserDto);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _usersService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserAsync(int Id)
        {
            var user = await _usersService.GetUser(Id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int Id)
        {
            await _usersService.DeleteUser(Id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int Id, UsersDto usersDto)
        {
            var result = await _usersService.UpdateUser(usersDto);
            return Ok(result);
        }
    }
}