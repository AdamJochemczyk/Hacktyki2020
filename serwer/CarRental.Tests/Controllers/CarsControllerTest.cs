using CarRental.API.Controllers;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Controllers
{
    public class CarsControllerTest
    {
        private readonly Mock<ICarService> mockService;
        private readonly CarsController controller;
        public CarsControllerTest()
        {
            mockService = new Mock<ICarService>();
            controller = new CarsController(mockService.Object);
        }

        [Fact]
        public async Task GetAllCarsAsync_WhenCalled_ReturnsOkResult()
        {
            mockService.Setup(p => p.GetAllCarsAsync())
                .ReturnsAsync(new CarDto[] {
                    new CarDto(),
                    new CarDto()
                });
            var result = await controller.GetAllCarsAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllCarsAsync_WhenCalled_ReturnsAllItems()
        {
            mockService.Setup(p => p.GetAllCarsAsync())
                .ReturnsAsync(new CarDto[] {
                    new CarDto(),
                    new CarDto()
                });
            var result = await controller.GetAllCarsAsync();
            var assertResult = Assert.IsType<OkObjectResult>(result);
            var cars = Assert.IsType<CarDto[]>(assertResult.Value);
            Assert.Equal(2, cars.Length);
        }

        [Fact]
        public async Task GetCarById_UnknowedIdPassed_ReturnsNotFoundResult()
        {
            int testId = 9000;
            mockService.Setup(p => p.GetCarByIdAsync(testId))
                .ReturnsAsync((CarDto)null);
            var result = await controller.GetCarByIdAsync(testId);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCarByIdAsync_ExistingIdPassed_ReturnsOkResult()
        {
            int testId = 1;
            mockService.Setup(p => p.GetCarByIdAsync(testId))
             .ReturnsAsync(new CarDto());
            var result = await controller.GetCarByIdAsync(testId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCarByIdAsync_ExistingIdPassed_ReturnsRightItem()
        {
            int testId = 1;
            CarDto car = new CarDto();
            mockService.Setup(p => p.GetCarByIdAsync(testId))
             .ReturnsAsync(car);
            var result = await controller.GetCarByIdAsync(testId);
            var assertResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<CarDto>(assertResult.Value);
            Assert.Equal(car, assertResult.Value);
        }

        [Fact]
        public async Task CreateCarAsync_ValidObjectPassed_ReturnsCreatedRespone()
        {
            CarCreateDto car = new CarCreateDto()
            {
                Brand = "Kia",
                Model = "Ceed",
                RegistrationNumber = "SZ78790",
                NumberOfDoor = 5,
                NumberOfSits = 5,
                YearOfProduction = 2018,
                TypeOfCar = DAL.Entities.CarType.Classic
            };
            mockService.Setup(p => p.CreateCarAsync(car))
                .ReturnsAsync(new CarDto());
            var result = await controller.CreateCarAsync(car);
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateCarAsync_InvalidObjectPassed_ReturnsBadRequest()
        {
            CarCreateDto missingValueCar = new CarCreateDto()
            {
                Model = "Ceed",
                RegistrationNumber = "SZ78790",
                NumberOfDoor = 5,
                NumberOfSits = 5,
                YearOfProduction = 2018,
                TypeOfCar = DAL.Entities.CarType.Classic
            };

            controller.ModelState.AddModelError("Brand", "Required");
            var result = await controller.CreateCarAsync(missingValueCar);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateCarAsync_BadIdPassed_ReturnsBadRequest()
        {
            CarDto car = new CarDto()
            {
                CarId = 14,
                Model = "Ceed",
                RegistrationNumber = "SZ78790",
                NumberOfDoor = 5,
                NumberOfSits = 5,
                YearOfProduction = 2018,
                TypeOfCar = DAL.Entities.CarType.Classic
            };
            var result = await controller.UpdateCarAsync(16, car);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateCarAsync_CorrectDataPassed_ReturnsOkResponse()
        {
            CarDto car = new CarDto()
            {
                CarId = 14,
                Model = "Ceed",
                RegistrationNumber = "SZ78790",
                NumberOfDoor = 5,
                NumberOfSits = 5,
                YearOfProduction = 2018,
                TypeOfCar = DAL.Entities.CarType.Classic
            };
            var result = await controller.UpdateCarAsync(14, car);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCarAsync_IncorrectIdPassed_ReturnsNotFound()
        {
            int testId = 222;
            var result = await controller.DeleteCarAsync(testId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCarAsync_CorrectIdPassed_ReturnsOkResult()
        {
            int testId = 222;
            mockService
                .Setup(p => p.GetCarByIdAsync(testId))
                .ReturnsAsync(new CarDto());
            var result = await controller.DeleteCarAsync(testId);
            Assert.IsType<OkResult>(result);
        }
    }
}

