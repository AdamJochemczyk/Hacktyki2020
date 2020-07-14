using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Reservation : BaseEntity
    {
        public int ReservationId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }

        public Car Car { get; set; }
        public User User { get; set; }

        public void Update(DateTime rentalDate, DateTime returnDate, int carId, int userId)
        {
            RentalDate = rentalDate;
            ReturnDate = returnDate;
            CarId = carId;
            UserId = userId;
        }
    }
}
