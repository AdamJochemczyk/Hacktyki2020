using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Interfaces;
using CarRental.DAL.Repositories;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Email_Templates;
using CarRental.Services.Models.Reservation;
using CarRental.Services.Models.User;
using CarRental.Services.Services;
using CarRental.Services.Validators;
using CarRental.Services.Mapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Models.Car;

namespace CarRental.API.StartupExtensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());
        }

        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<Profile, ReservationProfile>()
                .AddSingleton<Profile, UserProfile>()
                .AddSingleton<Profile, CarProfile>()
                .AddSingleton<IConfigurationProvider, AutoMapperConfiguration>(p =>
                    new AutoMapperConfiguration(p.GetServices<Profile>()))
                .AddSingleton<IMapper, Mapper>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IEmailServices, EmailService>();
<<<<<<< HEAD
            services.AddScoped<IAuthorizationService, AuthorizationService>();
=======
            services.AddScoped<ICarService, CarService>();
>>>>>>> master
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ReservationCreateDto>, ReservationCreateDtoValidator>();
            services.AddTransient<IValidator<ReservationUpdateDto>, ReservationUpdateDtoValidator>();
<<<<<<< HEAD
            services.AddTransient<IValidator<CreateUserDto>, CreateUserDtoValidator>();
            services.AddTransient<IValidator<UpdateUserPasswordDto>, UpdateUserPasswordValidator>();

=======
            services.AddTransient<IValidator<CarDto>, CarDtoValidator>();
            services.AddTransient<IValidator<CarCreateDto>, CarCreateDtoValidator>();
>>>>>>> master
            return services;
        }
    }
}
