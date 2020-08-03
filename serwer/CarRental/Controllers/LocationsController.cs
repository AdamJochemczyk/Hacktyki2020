using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Location;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : Controller
    {
        private readonly ILocationService service;
        public LocationsController(ILocationService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationByReservationIdAsync(int id)
        {
            var entity = await service.GetActualLocationByReservationIdAsync(id);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocationAsync(LocationCreateDto location)
        {
            var entity = await service.CreateLocationAsync(location);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationAsync(int id)
        {
            try { await service.DeleteLocationAsync(id); }
            catch (ArgumentNullException) { return BadRequest(); }
            return Ok();
        }
    }
}
