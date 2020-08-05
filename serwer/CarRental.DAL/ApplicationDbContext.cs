using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public  DbSet<RefreshToken> Refresh { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>().HasData(
                new Car
                {
                    CarId = 1,
                    Brand = "Audi",
                    Model = "Q5",
                    YearOfProduction = 2019,
                    ImagePath = "https://pngimg.com/uploads/audi/audi_PNG1737.png",
                    RegistrationNumber = "SZE4562",
                    NumberOfDoor = 5,
                    NumberOfSits = 5,
                    TypeOfCar = CarType.Classic,
                    DateCreated = DateTime.Now
                });

            builder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateCreated = DateTime.Now
                });

            builder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = 1,
                    RentalDate = DateTime.Now.AddDays(2),
                    ReturnDate = DateTime.Now.AddDays(5),
                    IsFinished = false,
                    CarId = 1,
                    UserId = 1,
                    DateCreated = DateTime.Now
                });

            builder.Entity<Location>().HasData(
                new Location
                {
                    LocationId = 1,
                    ReservationId = 1,
                    Latitude = 50.50,
                    Longitude = 43.30,
                    IsActual = true,
                    DateCreated = DateTime.Now
                });
        }
    }
}
