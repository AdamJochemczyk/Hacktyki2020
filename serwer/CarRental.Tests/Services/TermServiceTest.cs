using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CarRental.Services.Services;
using System.Linq;

namespace CarRental.Tests.Services
{
    public class TermServiceTest
    {
        private readonly Mock<IReservationRepository> mockRepository;
        public TermServiceTest()
        {
            mockRepository = new Mock<IReservationRepository>();
        }

        [Fact]
        public async Task GetFreeTermsByCarIdAsync_IdPassed_Returns()
        {
            //Arrange
            int id = 1;
            var reservations = new List<Reservation>() {
                new Reservation()
                {
                    RentalDate = DateTime.Now.AddDays(2),
                    ReturnDate = DateTime.Now.AddDays(6)
                },
                new Reservation()
                {
                    RentalDate = DateTime.Now.AddDays(8),
                    ReturnDate = DateTime.Now.AddDays(13)
                }
            };
            var dates = new List<string>()
            {
                DateTime.Now.Date.ToString("dd.MM.yyyy"),
                DateTime.Now.AddDays(1).Date.ToString("dd.MM.yyyy"),
                DateTime.Now.AddDays(7).Date.ToString("dd.MM.yyyy")              
            };
            mockRepository
                .Setup(p => p.FindAllByCarIdAsync(id))
                .ReturnsAsync(reservations);
            var service = new TermService(mockRepository.Object);
            //Act
            var returnedDates = await service.GetFreeTermsByCarIdAsync(id, null, null);
            //Assert
            Assert.Equal(dates, returnedDates);
        }

        [Fact]
        public async Task GetFreeTermsByCarIdAsync_IdAndDatesPassed_Returns() 
        {
            //Arrange
            int id = 1;
            var reservations = new List<Reservation>() {
                new Reservation()
                {
                    RentalDate = DateTime.Now.AddDays(2),
                    ReturnDate = DateTime.Now.AddDays(6)
                },
                new Reservation()
                {
                    RentalDate = DateTime.Now.AddDays(8),
                    ReturnDate = DateTime.Now.AddDays(13)
                }
            };
            var dates = new List<string>()
            {
                DateTime.Now.Date.ToString("dd.MM.yyyy"),
                DateTime.Now.AddDays(1).Date.ToString("dd.MM.yyyy"),
                DateTime.Now.AddDays(7).Date.ToString("dd.MM.yyyy"),
                DateTime.Now.AddDays(14).Date.ToString("dd.MM.yyyy")
            };
            mockRepository
                .Setup(p => p.FindAllByCarIdAsync(id))
                .ReturnsAsync(reservations);
            var service = new TermService(mockRepository.Object);
            //Act
            var returnedDates = await service.GetFreeTermsByCarIdAsync(id, DateTime.Now.AddDays(6), DateTime.Now.AddDays(13));
            //Assert
            Assert.Equal(dates, returnedDates);
        }

        [Fact]
        public void PrepareFreeDaysArray_FutureDatesPassed_ReturnsCorrectDaysOfYear() 
        {
            //Arrange
            var rentalDate = DateTime.Now.AddDays(8);
            var returnDate = DateTime.Now.AddDays(12);
            var freeDays = Enumerable.Range(rentalDate.DayOfYear - 7, returnDate.DayOfYear - rentalDate.DayOfYear + 8);
            var service = new TermService(mockRepository.Object);
            //Act
            var result = service.PrepareFreeDaysArray(rentalDate, returnDate);
            //Assert
            Assert.Equal(freeDays, result);
        }

        [Fact]
        public void PrepareFreeDaysArray_NullsPassed_ReturnsTwoWeeksAsDaysOfYear() 
        {
            //Arrange
            var freeDays = Enumerable.Range(DateTime.Now.DayOfYear, 14);
            var service = new TermService(mockRepository.Object);
            //Act
            var result = service.PrepareFreeDaysArray(null, null);
            //Assert
            Assert.Equal(freeDays, result);
        }
    }
}
