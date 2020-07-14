using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Location : BaseEntity
    {
        public int LocationId { get; set; }
        public int CarId { get; set; }
        public string Position { get; set; }

        public Car Car { get; set; }
    }
}
