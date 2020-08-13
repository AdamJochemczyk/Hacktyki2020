using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;
        public CarService(
            ICarRepository carRepository,
            IMapper mapper)
        {
            this.carRepository = carRepository;
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
                ImagePath = carDto.ImagePath,
                DateCreated = DateTime.Now
            };
            carRepository.Create(car);
            await carRepository.SaveChangesAsync();
            car = await carRepository.FindByIdAsync(car.CarId);
            return mapper.Map<CarDto>(car);
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await carRepository.FindByIdAsync(id);
            car.IsDeleted = true;
            await carRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var cars = await carRepository.FindAllAsync();
            return mapper.Map<IEnumerable<CarDto>>(cars);
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var car = await carRepository.FindByIdAsync(id);
            return mapper.Map<CarDto>(car);
        }

        public async Task<CarDto> UpdateCarAsync(CarDto carDto)
        {
            var car = await carRepository.FindByIdAsync(carDto.CarId);
            car.Update(carDto.Brand, carDto.Model, carDto.RegistrationNumber, carDto.TypeOfCar, carDto.NumberOfDoor,
            carDto.NumberOfSits, carDto.YearOfProduction, carDto.ImagePath);
            carRepository.Update(car);
            await carRepository.SaveChangesAsync();
            car = await carRepository.FindByIdAsync(carDto.CarId);
            return mapper.Map<CarDto>(car);
        }

        public async Task<IEnumerable<CarDto>> GetAvailableCars(DateTime rentalDate, DateTime returnDate)
        {
            var reservedCars = await carRepository.GetReservedCarsByDates(rentalDate, returnDate);
            var allCars = await carRepository.FindAllAsync();
            List<Car> availableCars = allCars.Except(reservedCars).ToList();
            return mapper.Map<IEnumerable<CarDto>>(availableCars);
        }
    }
}
