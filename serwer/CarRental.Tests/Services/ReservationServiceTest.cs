﻿using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.Reservation;
using CarRental.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> mockRepository;
        private readonly IMapper mapper;
        public ReservationServiceTest()
        {
            mockRepository = new Mock<IReservationRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Reservation, ReservationDto>();
            });
            mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllReservationsAsync_WhenCalled_ReturnsAllObjects()
        {
            //Arrange
            List<Reservation> reservations = new List<Reservation>() { new Reservation(), new Reservation() };
            mockRepository
                .Setup(p => p.FindAllAsync())
                .ReturnsAsync(reservations);
            var service = new ReservationService(mockRepository.Object, mapper);
            //Act
            var result = await service.GetAllReservationsAsync();
            //Assert
            var assertResult = Assert.IsType<List<ReservationDto>>(result);
            Assert.Equal(reservations.Count, assertResult.Count);
        }

        [Fact]
        public async Task GetReservationByIdAsync_ExistingIdPassed_ReturnsCorrectObjects()
        {
            //Arrange
            int id = 1;
            mockRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(new Reservation() { ReservationId = id });
            var service = new ReservationService(mockRepository.Object, mapper);
            //Act
            var result = await service.GetReservationByIdAsync(id);
            //Assert
            var assertResult = Assert.IsType<ReservationDto>(result);
            Assert.Equal(id, assertResult.ReservationId);
        }

        [Fact]
        public async Task GetReservationByIdAsync_UnexistingIdPassed_ReturnsCorrectObjects()
        {
            //Arrange
            int id = 1;
            mockRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync((Reservation)null);
            var service = new ReservationService(mockRepository.Object, mapper);
            //Act
            var result = await service.GetReservationByIdAsync(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateReservationAsync_ValidObjectPassed_CreatedObject()
        {
            Reservation reservation = new Reservation() { ReservationId = 1 };
            ReservationCreateDto reservationDto = new ReservationCreateDto();
            mockRepository
                .Setup(p => p.FindByIdAsync(0))
                .ReturnsAsync(reservation);
            var service = new ReservationService(mockRepository.Object, mapper);
            //Act
            var result = await service.CreateReservationAsync(reservationDto);
            //Assert
            Assert.Equal(result.ReservationId, reservation.ReservationId);
            Assert.IsType<ReservationDto>(result);
        }

        [Fact]
        public async Task UpdateCarAsync_PassedValidObject_ReturnsObject()
        {
            //Arrange
            ReservationUpdateDto reservationDto = new ReservationUpdateDto()
            {
                ReservationId = 1,
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(5),
                IsFinished = true
            };
            Reservation reservation = new Reservation() { 
                ReservationId = 1,
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(10),
                IsFinished = false
            };

            mockRepository
                .Setup(p => p.FindByIdAsync(reservationDto.ReservationId))
                .ReturnsAsync(reservation);
            var service = new ReservationService(mockRepository.Object, mapper);
            //Act
            var result = await service.UpdateReservationAsync(reservationDto);
            //Assert
            Assert.Equal(result.ReservationId, reservationDto.ReservationId);
            Assert.Equal(result.RentalDate, reservationDto.RentalDate);
            Assert.Equal(result.ReturnDate, reservationDto.ReturnDate);
            Assert.Equal(result.IsFinished, reservationDto.IsFinished);
            Assert.IsType<ReservationDto>(result);
        }
    }
}
