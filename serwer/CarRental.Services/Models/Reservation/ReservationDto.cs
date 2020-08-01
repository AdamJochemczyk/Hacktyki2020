﻿
using CarRental.Services.Models.Car;
using CarRental.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.Reservation
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsFinished { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
        public CarDto Car { get; set; }
        public UsersDto User { get; set; }
    }
}
