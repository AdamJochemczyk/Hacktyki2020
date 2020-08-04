using CarRental.DAL;
using CarRental.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Repositories
{
    public class ReservationRepositoryTest
    {
        private readonly Mock<ApplicationDbContext> mockContext;
        public ReservationRepositoryTest()
        {
            mockContext = new Mock<ApplicationDbContext>();
        }
        [Fact]
        public async Task FindAllAsync_WhenCalled_ReturnsObjectsWithUserAndCars()
        {
            var repository = new ReservationRepository(mockContext.Object);
            var result = await repository.FindAllAsync();

        }
    }
}
