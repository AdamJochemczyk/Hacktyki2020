using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Car : BaseEntity
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public string ImagePath { get; set; }
    }
}
