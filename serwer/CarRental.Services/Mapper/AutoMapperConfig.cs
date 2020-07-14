using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.MapperProfiles
{
    public class AutoMapperConfiguration : MapperConfiguration
    {
        public AutoMapperConfiguration(IEnumerable<Profile> profiles) : base(cfg =>
        {
            foreach (var profile in profiles)
                cfg.AddProfile(profile);
        })
        {
        }
    }
}
