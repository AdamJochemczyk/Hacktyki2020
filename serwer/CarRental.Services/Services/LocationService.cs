using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Location;
using System;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;
        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            this.locationRepository = locationRepository;
            this.mapper = mapper;
        }

        public async Task<LocationDto> GetActualLocationByReservationIdAsync(int id)
        {
            var location = await locationRepository.GetActualLocationByReservationIdAsync(id);
            return mapper.Map<LocationDto>(location);
        }

        public async Task<LocationDto> GetLocationByIdAsync(int id)
        {
            var location = await locationRepository.FindByIdAsync(id);
            return mapper.Map<LocationDto>(location);
        }

        public async Task<LocationDto> CreateLocationAsync(LocationCreateDto locationCreateDto)
        {
            var oldLocation = await locationRepository
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
            locationRepository.Create(location);
            await locationRepository.SaveChangesAsync();
            return mapper.Map<LocationDto>(location);
        }

        public async Task DeleteLocationAsync(int id)
        {
            var location = await locationRepository.FindByIdAsync(id);
            location.IsActual = false;
            await locationRepository.SaveChangesAsync();
        }
    }
}
