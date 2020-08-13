using CarRental.API.Attributes;
using CarRental.API.Resources;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Resources;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        public ResourceManager resourcesManager;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }

        [HttpGet]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await usersService.GetAllUsersAsync();
            if (result == null)
            {
                return NotFound(resourcesManager.GetString("DatabaseEmpty"));
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetUserAsync(int id)
        {

            var user = await usersService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            user = await usersService.GetUserAsync(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {

            if (!await usersService.DeleteUserAsync(id))
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            return Ok();
        }


        [HttpPatch("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> UpdateUserAsync(int id, UsersDto usersDto)
        {
            if (id != usersDto.UserId)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            var result = await usersService.UpdateUserAsync(usersDto);
            if (result.isValid == false)
            {
                return NotFound(resourcesManager.GetString("NotFound"));
            }
            return Ok(result);
        }
    }
}