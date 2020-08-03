using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Defect;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Mapper
{
    public class DefectProfile : Profile
    {
        public DefectProfile()
        {
            CreateMap<Defect, DefectDto>();
        }
    }
}
