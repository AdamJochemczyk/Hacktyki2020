﻿using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Email_Templates;
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
    public class AuthorizationServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IRefreshRepository> _mockRefreshRepository;
        private readonly IMapper _mapper;
        public AuthorizationServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockRefreshRepository = new Mock<IRefreshRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<User, CreateUserDto>();
            });
            _mapper = config.CreateMapper();
        }
        [Fact]
        public async Task RegistrationUserAsync_EmailCorrect_UserNull_ReturnMapUser()
        {
            //Arrange
            CreateUserDto createUser = new CreateUserDto
            {
                FirstName = "Bohdan",
                LastName = "Kucher",
                NumberIdentificate = "123123",
                Email = "kucherbogdan200@gmail.com",
                MobileNumber = "123123123"
            };
            User user = new User();
            _mockUserRepository
                .Setup(p => p.Create(It.IsAny<User>()))
                .Verifiable();
            _mockUserRepository
                .Setup(p => p.FindByLogin(createUser.Email))
                .ReturnsAsync(null as User);
            _mockUserRepository
                .Setup(p => p.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var emailService = new Mock<IEmailServices>();
            emailService
                .Setup(p => p.EmailAfterRegistration(It.IsAny<CreateUserDto>()))
                .Returns(true);
            var tokenService = new Mock<ITokenGeneratorService>();
            var services = new AuthorizationService(_mockUserRepository.Object, emailService.Object, _mapper,
                                                     tokenService.Object, _mockRefreshRepository.Object);
            //Act
            var result = await services.RegistrationUserAsync(createUser);
            //Assert
            emailService.Verify(e => e.EmailAfterRegistration(It.IsAny<CreateUserDto>()), Times.Once);
        }
        [Fact]
        public async Task RegistrationUserAsync_EmailNotCorrect_UserNotNull_ReturnCreateUserDto()
        {
            //Arrange
            CreateUserDto createUser = new CreateUserDto
            {
                FirstName = "Bohdan",
                LastName = "Kucher",
                NumberIdentificate = "123123",
                Email = "kucherbogdan200@gmail.com",
                MobileNumber = "123123123"
            };
            _mockUserRepository
               .Setup(p => p.FindByLogin(createUser.Email))
               .ReturnsAsync(new User());
            var emailService = new Mock<IEmailServices>();
            emailService
                .Setup(p => p.EmailAfterRegistration(It.IsAny<CreateUserDto>()))
                .Returns(true);
            var tokenService = new Mock<ITokenGeneratorService>();
            var services = new AuthorizationService(_mockUserRepository.Object, emailService.Object, _mapper,
                                                     tokenService.Object, _mockRefreshRepository.Object);
            //Act 
            var result = await services.RegistrationUserAsync(createUser);
            //Assert
            emailService.Verify(e => e.EmailAfterRegistration(It.IsAny<CreateUserDto>()), Times.Never);
        }
        [Fact]
        public async Task SetPassword_ReturnTrue_IfCorrect()
        {
            //Arrange
            var updateUser = new UpdateUserPasswordDto()
            {
                EncodePassword = "basket",
                ConfirmEncodePassword = "basket",
                CodeOfVerification = "23432knksjcnfi2"
            };
            _mockUserRepository
                .Setup(p => p.FindByCodeOfVerification(updateUser.CodeOfVerification))
                .ReturnsAsync(new User());
            _mockUserRepository
                .Setup(p => p.Create(It.IsAny<User>()))
                .Verifiable();
            _mockUserRepository
                .Setup(p => p.SaveChangesAsync())
                .Returns(Task.CompletedTask);
            var emailService = new Mock<IEmailServices>();
            emailService
                .Setup(p => p.EmailAfterRegistration(It.IsAny<CreateUserDto>()))
                .Returns(true);
            var tokenService = new Mock<ITokenGeneratorService>();
            var services = new AuthorizationService(_mockUserRepository.Object, emailService.Object, _mapper,
                                                     tokenService.Object, _mockRefreshRepository.Object);
            //Act
            var result = await services.SetPassword(updateUser);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task SignIn_LoginNotFound_ReturnErrorTokenDto()
        {
            //Arrange
            var signIn = new UserLoginDto()
            {
                Email = "kucherbogdan@gmail.com",
                EncodePassword = "Basket",
                RoleOfWorker = 1
            };
            _mockUserRepository
                .Setup(p => p.FindByLogin(signIn.Email))
                .ReturnsAsync(null as User);
            var emailService = new Mock<IEmailServices>();
            emailService
                .Setup(p => p.EmailAfterRegistration(It.IsAny<CreateUserDto>()))
                .Returns(true);
            var tokenService = new Mock<ITokenGeneratorService>();
            var services = new AuthorizationService(_mockUserRepository.Object, emailService.Object, _mapper,
                                                     tokenService.Object, _mockRefreshRepository.Object);
            //Act 
            var result = await services.SignIn(signIn);
            //Assert
            Assert.Equal(401, result.Code);
        }
        [Fact]
        public async Task SignIn_PasswordNotCorrect_ReturnErrorTokenDto()
        {
            //Arrange
            var signIn = new UserLoginDto()
            {
                Email = "kucherbogdan@gmail.com",
                EncodePassword = "Basket",
                RoleOfWorker = 1
            };
            _mockUserRepository
                .Setup(p => p.FindByLogin(signIn.Email))
                .ReturnsAsync(new User());
            var emailService = new Mock<IEmailServices>();
            emailService
                .Setup(p => p.EmailAfterRegistration(It.IsAny<CreateUserDto>()))
                .Returns(true);
            var tokenService = new Mock<ITokenGeneratorService>();
            var services = new AuthorizationService(_mockUserRepository.Object, emailService.Object, _mapper,
                                                     tokenService.Object, _mockRefreshRepository.Object);
            //Act 
            var result = await services.SignIn(signIn);
            //Assert
            Assert.Equal(401, result.Code);
        }
        [Fact]
        public async Task SignIn_LoginPasswordCorrect_ReturnToken()
        {
            //Arrange
            var signIn = new UserLoginDto()
            {
                Email = "kucherbogdan@gmail.com",
                EncodePassword = "12345678",
                RoleOfWorker = 1
            };
            _mockUserRepository
                .Setup(p => p.FindByLogin(signIn.Email))
                .ReturnsAsync(new User()
                {
                    Email="kucherbogdan@gmail.com",
                    HashPassword= "Y1h8fxss+YHLfAKsjtsWh2iajVCSgr3o0BzkzyGtkwkmlx8e86zDaX6n/f90pCq7q//FAqY81j4APQkAiojqWTjIP/V8rxNE8i3pvkphv6LQbp5N6CKKOOZhm1HPLsWUfeBCFQXCkYU6bKvE2z3ZneygqIX+1hz72A4gSdo+HlO+wEbBusPcFbtUw5vLqH66FriJlKCC0wu7gTol+BHyZmlVMI3b2suNjF1vBczwXhl2m2xqNJbICDvPZOx/+s6yMzgbgZcedpeddwtARmwJGf1L3JN8VtHp52KpkrdZlto6zP5meBrSghXZz5DQyV2vUwxl1PSlLcAEzRRDM/bkSw==",
                    Salt= "bEa2vi+yUMDAar64EyKIgA=="
                });
            var emailService = new Mock<IEmailServices>();
            emailService
                .Setup(p => p.EmailAfterRegistration(It.IsAny<CreateUserDto>()))
                .Returns(true);
            var tokenService = new Mock<ITokenGeneratorService>();
            var services = new AuthorizationService(_mockUserRepository.Object, emailService.Object, _mapper,
                                                     tokenService.Object, _mockRefreshRepository.Object);
            //Act 
            var result = await services.SignIn(signIn);
            //Assert
            Assert.Equal(200, result.Code);
        }
    }
}
