using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Entities
{
    public class Defect : BaseEntity
    {
        public int DefectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationNumber{get;set;}
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReport { get; set; } 
        public int CarId { get; set; }
        public Status Status { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }

        public Defect() { }
        public Defect(int userId , string name,string surname , string registrationNumber,
                                string description, DateTime dateTimeReport,Status status)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            RegistrationNumber =registrationNumber ;
            Description = description;
            DateOfReport = dateTimeReport;
            Status = status;

        }
    }
}
