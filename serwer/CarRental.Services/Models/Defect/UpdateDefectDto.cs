using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.Defect
{
    public class UpdateDefectDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

    }
}
