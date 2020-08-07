using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.Defect
{
    public class DefectDto
    {
        public int DefectId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public Status Status { get; set; }
        public DateTime DateOfReport { get; set; }
        public string Description { get; set; }
    }
}
