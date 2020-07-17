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
                    DateCreated = DateTime.Now
                });

            builder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateCreated = DateTime.Now
                }
                //,
            //    new User
            //    {
            //        UserId = 2,
            //        FirstName = "James",
            //        LastName = "Doe",
            //        DateCreated = DateTime.Now
            //    }
               );
        }
    }
}
