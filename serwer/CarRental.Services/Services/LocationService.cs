using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Location;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository repository;
        private readonly IMapper mapper;
        public LocationService(ILocationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<LocationDto> GetActualLocationByReservationIdAsync(int id)
        {
            var location = await repository.GetActualLocationByReservationIdAsync(id);
            return mapper.Map<LocationDto>(location);
        }

        public async Task<LocationDto> GetLocationByIdAsync(int id)
        {
            var location = await repository.FindByIdAsync(id);
            return mapper.Map<LocationDto>(location);
        }

        public async Task<LocationDto> CreateLocationAsync(LocationCreateDto locationCreateDto)
        {
            var oldLocation = await repository
                .GetActualLocationByReservationIdAsync(locationCreateDto.ReservationId);
            if (oldLocation != null)
                oldLocation.IsActual = false;

            Location location = new Location()
            {
                ReservationId = locationCreateDto.ReservationId,
                Latitude = locationCreateDto.Latitude,
                Longitude = locationCreateDto.Longitude,
                IsActual = true,
                DateCreated = DateTime.Now
            };
            repository.Create(location);
            await repository.SaveChangesAsync();
            return mapper.Map<LocationDto>(location);
        }

        public async Task DeleteLocationAsync(LocationDto location)
        {
            location.IsActual = false;
            await repository.SaveChangesAsync();
        }
    }
}
