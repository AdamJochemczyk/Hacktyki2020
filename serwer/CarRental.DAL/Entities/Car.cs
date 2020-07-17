using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Entities
{
    public class Car : BaseEntity
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }

        public CarType TypeOfCar { get; set; }
        public int NumberOfDoor { get; set; }
        public int NumberOfSits { get; set; }
        public int YearOfProduction { get; set; }
        public string ImagePath { get; set; }

        public void Update(string brand, string model, string registrationNumber, CarType type, int numberOfDoor,
           int numberOfSits, int yearOfProduction, string imagePath)
        {
            this.Brand = brand;
            this.Model = model;
            this.RegistrationNumber = registrationNumber;
            this.TypeOfCar = type;
            this.NumberOfDoor = numberOfDoor;
            this.NumberOfSits = numberOfSits;
            this.YearOfProduction = yearOfProduction;
            this.ImagePath = imagePath;
        }
    }
}
