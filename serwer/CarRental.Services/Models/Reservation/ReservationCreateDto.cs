﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.Reservation
{
    public class ReservationCreateDto
    {
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
    }
}