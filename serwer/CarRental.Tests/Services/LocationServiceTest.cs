using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.Location;
using CarRental.Services.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class LocationServiceTest
    {
        private readonly Mock<ILocationRepository> mockRepository;
        private readonly IMapper mapper;
        public LocationServiceTest()
        {
            mockRepository = new Mock<ILocationRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Location, LocationDto>();
            });
            mapper = config.CreateMapper();
        }

        [Fact]
        public async Task CreateLocationAsync_PassedValidObject_ReturnsObject()
        {
            //Arrange
            var oldLocation = new Location() { ReservationId = 1 };
            var locationDto = new LocationCreateDto() { ReservationId = 1 };
            mockRepository
                .Setup(p => p
                .GetActualLocationByReservationIdAsync(oldLocation.ReservationId))
                .ReturnsAsync(oldLocation);
            var service = new LocationService(mockRepository.Object, mapper);
            //Act
            var newLocation = await service.CreateLocationAsync(locationDto);
            //Assert
            Assert.Equal(newLocation.ReservationId, locationDto.ReservationId);
            Assert.Equal(newLocation.LocationId, oldLocation.LocationId);
            Assert.True(newLocation.IsActual);
            Assert.False(oldLocation.IsActual);
            Assert.IsType<LocationDto>(newLocation);
        }
    }
}
