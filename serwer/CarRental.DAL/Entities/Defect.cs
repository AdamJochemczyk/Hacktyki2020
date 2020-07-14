using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Defect : BaseEntity
    {
        public int DefectId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public Status Status { get; set; }

        public Car Car { get; set; }
        public User User { get; set; }
    }
}
