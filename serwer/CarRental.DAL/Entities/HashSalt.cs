using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.DAL.Entities
{
    public class HashSalt
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
