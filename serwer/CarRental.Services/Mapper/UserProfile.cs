using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Mapper
{
   public class UserProfile:Profile
    {
        public UserProfile()
        {
           CreateMap<User, CreateUserDto>();
           
            CreateMap<User,UsersDto>().ReverseMap();
        }
    }
}
