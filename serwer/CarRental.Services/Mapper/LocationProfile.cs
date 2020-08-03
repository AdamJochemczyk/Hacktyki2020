using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Mapper
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDto>();
        }
    }
}
