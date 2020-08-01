using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.HashPassword
{
    public class HashSaltDto
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
