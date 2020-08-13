using CarRental.API.Attributes;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : Controller
    {
        private readonly ILocationService locationService;
        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetLocationByReservationIdAsync(int id)
        {
            var location = await locationService.GetActualLocationByReservationIdAsync(id);
            return Ok(location);
        }

        [HttpPost]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> CreateLocationAsync(LocationCreateDto locationDto)
        {
            var location = await locationService.CreateLocationAsync(locationDto);
            return Ok(location);
        }

        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteLocationAsync(int id)
        {
            var location = await locationService.GetActualLocationByReservationIdAsync(id);
            if (location != null)
            {
                await locationService.DeleteLocationAsync(id);
            }
            return Ok();
        }
    }
}
