using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.User;
using CarRental.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mockRepository;
        private readonly IMapper _mapper;
        public UserServiceTest()
        {
            _mockRepository = new Mock<IUserRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<User, UsersDto>();
            });
            _mapper = config.CreateMapper();
        }
        [Fact]
        public async Task GetAllUsers_ReturnAllUser()
        {
            //Arrange
            List<User> users = new List<User> { new User(), new User() };
            _mockRepository
                .Setup(p => p.FindAllUsers())
                .ReturnsAsync(users);
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.GetAllUsers();
            //Assert
            var assertResult = Assert.IsType<List<UsersDto>>(result);
            Assert.Equal(users.Count, assertResult.Count);
        }
        [Fact]
        public async Task GetUserById_ExcitingId_ReturnCorrectObject()
        {
            //Arrange
            int id = 1;
            _mockRepository
                .Setup(p => p.FindByIdDetails(id))
                .ReturnsAsync(new User { UserId = 1 });
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.GetUser(id);
            //Assert
            var assertResult = Assert.IsType<UsersDto>(result);
            Assert.Equal(id, assertResult.UserId);
        }
        [Fact]
        public async Task GetUserById_NotExcitingId_ReturnsNull()
        {
            int id = 1;
            _mockRepository
                .Setup(p => p.FindByIdDetails(id))
                .ReturnsAsync(new User { UserId = 1 });
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.GetUser(id);
            //Assert
            Assert.Null(result);
        }
    }
}
