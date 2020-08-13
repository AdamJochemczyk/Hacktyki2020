using CarRental.API.Attributes;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly ICarService carService;
        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var cars = await carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{id}", Name = "GetById")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetCarByIdAsync(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            return Ok(car);
        }

        [HttpPost]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> CreateCarAsync(CarCreateDto carDto)
        {
            var car = await carService.CreateCarAsync(carDto);
            if (car == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute(
                routeName: "GetById",
                routeValues: new { id = car.CarId },
                value: car);
        }

        [HttpPatch("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> UpdateCarAsync(int id, CarDto carDto)
        {
            if (id != carDto.CarId)
            {
                return BadRequest();
            }
            var result = await carService.UpdateCarAsync(carDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteCarAsync(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            if (car != null && car.IsDeleted != true)
            {
                await carService.DeleteCarAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}