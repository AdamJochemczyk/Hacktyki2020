using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.MapperProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>();
        }
    }
}
