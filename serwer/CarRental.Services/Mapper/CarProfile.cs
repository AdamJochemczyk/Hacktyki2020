using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Mapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();
        }
    }
}
