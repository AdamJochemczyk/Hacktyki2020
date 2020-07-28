using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository repository;
        private readonly IMapper mapper;
        public CarService(
            ICarRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CarDto> CreateCarAsync(CarCreateDto carDto)
        {
            Car car = new Car()
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                RegistrationNumber = carDto.RegistrationNumber,
                TypeOfCar = carDto.TypeOfCar,
                NumberOfDoor = carDto.NumberOfDoor,
                NumberOfSits = carDto.NumberOfSits,
                YearOfProduction = carDto.YearOfProduction,
                ImagePath = carDto.ImagePath
            };
            repository.Create(car);
            await repository.SaveChangesAsync();
            var entity = await repository.FindByIdAsync(car.CarId);
            return mapper.Map<CarDto>(entity);
        }

        public async Task DeleteCar(int id)
        {
            var entity = await repository.FindByIdAsync(id);
            repository.Delete(entity);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var entities = await repository.FindAllAsync();
            return mapper.Map<IEnumerable<CarDto>>(entities);
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var entity = await repository.FindByIdAsync(id);
            return mapper.Map<CarDto>(entity);
        }

        public async Task<CarDto> UpdateCarAsync(CarDto carDto)
        {
            var car = await repository.FindByIdAsync(carDto.CarId);
            car.Update(carDto.Brand, carDto.Model, carDto.RegistrationNumber, carDto.TypeOfCar, carDto.NumberOfDoor,
            carDto.NumberOfSits, carDto.YearOfProduction, carDto.ImagePath);
            repository.Update(car);
            await repository.SaveChangesAsync();
            var entity = await repository.FindByIdAsync(carDto.CarId);
            return mapper.Map<CarDto>(entity);
        }

        public async Task<IEnumerable<CarDto>> GetAvailableCars(DateTime rentalDate, DateTime returnDate)
        {
            var entities = await repository.GetAvailableCars(rentalDate, returnDate);
            return mapper.Map<IEnumerable<CarDto>>(entities);
        }
    }
}
