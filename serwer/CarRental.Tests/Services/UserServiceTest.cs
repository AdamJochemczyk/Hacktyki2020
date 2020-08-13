using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.User;
using CarRental.Services.Services;
using Moq;
using System.Collections.Generic;
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
            //Arrange
            int id = 1;
            _mockRepository
                .Setup(p => p.FindByIdDetails(id))
                .ReturnsAsync((User)null);
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.GetUser(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_CorrectObject_ReturnObject()
        {
            //Arrange
            UsersDto usersDto = new UsersDto()
            {
                UserId = 1,
                FirstName = "Bohdan",
                Email = "kucher@gmail.com",
                LastName = "Kucher",
                MobileNumber = "123123123",
                IdentificationNumber = "123123"
            };
            User user = new User()
            {
                UserId = 1,
                FirstName = "Bogdan",
                Email = "kucher@gmail.com",
                LastName = "Kuchera",
                MobileNumber = "444444444",
                IdentificationNumber = "123123"
            };
            _mockRepository
                .Setup(p => p.FindByIdDetails(1))
                .ReturnsAsync(user);
            _mockRepository
                .Setup(u => u.Update(user))
                .Verifiable();
            _mockRepository
                .Setup(u => u.SaveChangesAsync())
                .Verifiable();
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.UpdateUser(usersDto);
            Assert.Equal(result.UserId, usersDto.UserId);
            Assert.Equal(result.FirstName, usersDto.FirstName);
            Assert.Equal(result.LastName, usersDto.LastName);
            Assert.Equal(result.MobileNumber, usersDto.MobileNumber);
            Assert.Equal(result.IdentificationNumber, usersDto.IdentificationNumber);
        }

        [Fact]
        public async Task UpdateUser_UserNotFound_ReturnObject()
        {
            //Arrange
            UsersDto usersDto = new UsersDto()
            {
                UserId = 1,
                FirstName = "Bohdan",
                Email = "kucher@gmail.com",
                LastName = "Kucher",
                MobileNumber = "123123123",
                IdentificationNumber = "123123"
            };
            _mockRepository
                .Setup(p => p.FindByIdDetails(usersDto.UserId))
                .ReturnsAsync(null as User);
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.UpdateUser(usersDto);
            //Assert
            Assert.False(result.isValid);
        }

        [Fact]
        public async Task DeleteUser_IdExiciting_ReturnTrue()
        {
            //Arrange
            int id = 2;
            _mockRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(new User()
                {
                    UserId = 2,
                    FirstName = "Bohdan"
                });
            _mockRepository
              .Setup(s => s.SaveChangesAsync())
              .Verifiable();
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.DeleteUser(id);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUser_IdNotFound_ReturnTrue()
        {
            //Arrange
            int id = 2;
            _mockRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(null as User);
            var services = new UsersService(_mockRepository.Object, _mapper);
            //Act
            var result = await services.DeleteUser(id);
            //Assert
            Assert.False(result);
        }
    }
}
