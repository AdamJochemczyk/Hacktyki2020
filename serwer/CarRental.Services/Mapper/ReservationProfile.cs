﻿using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Mapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>();
        }
    }
}
