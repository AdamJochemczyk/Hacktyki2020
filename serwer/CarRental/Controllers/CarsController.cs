using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
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
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var entities = await service.GetAllCarsAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarByIdAsync(int id)
        {
            var entity = await service.GetCarByIdAsync(id);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatAsync(CarCreateDto carDto)
        {
            var result = await service.CreateCarAsync(carDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarAsync(int id, CarDto carDto)
        {
            if (id != carDto.CarId) 
                return BadRequest();
            var result = await service.UpdateCarAsync(carDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAsync(int id)
        {
            await service.DeleteCar(id);
            return Ok();
        }
    }
}