﻿using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class TokenGeneratorServiceTest
    {
        public TokenGeneratorServiceTest() { }
        [Fact]
        public void GenerateToken_ExcitingId_ReturnToken()
        {
            //Arrange
            User user = new User()
            {
                UserId=12,
                HashPassword="12312eewsddf2323",
                Email = "kucher@gmail.com",
                MobileNumber = "123123123",
                NumberIdentificate = "123123"
            };
            var services = new TokenGeneratorService();
            //Act
            var result = services.GenerateToken(user);
            //Assert
            var assertResult = Assert.IsType<string>(result);
            Assert.Equal(result, assertResult);
        }
        [Fact]
        public void GenerateRefreshToken_ReturnRefreshToken()
        {
            //Arrange
            var services = new TokenGeneratorService();
            //Act
            var result = services.RefreshGenerateToken();
            //Assert
            var assertResult = Assert.IsType<string>(result);
            Assert.Equal(result, assertResult);
        }
    }
}
