using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly ICarService service;
        public CarsController(ICarService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var entities = await service.GetAllCarsAsync();
            return Ok(entities);
        }

        [HttpGet("{id}", Name = "GetById")]
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> GetCarByIdAsync(int id)
        {
            var entity = await service.GetCarByIdAsync(id);
            return Ok(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCarAsync(CarCreateDto carDto)
        {
            var result = await service.CreateCarAsync(carDto);
            if (result == null)
                return BadRequest();

            return CreatedAtRoute(
                routeName: "GetById",
                routeValues: new { id = result.CarId },
                value: result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCarAsync(int id, CarDto carDto)
        {
            if (id != carDto.CarId)
                return BadRequest();
            var result = await service.UpdateCarAsync(carDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCarAsync(int id)
        {
            var entity = await service.GetCarByIdAsync(id);
            if (entity != null && entity.IsDeleted != true)
            {
                await service.DeleteCar(entity);
                return Ok();
            }
            return BadRequest();
        }
    }
}