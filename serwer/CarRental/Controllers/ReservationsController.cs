﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : Controller
    {
        private readonly IReservationService service;
        public ReservationsController(IReservationService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var result = await service.GetAllReservationsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await service.GetReservationByIdAsync(id);
            return Ok(result);
        }

        [HttpGet, Route("cars/{id}")]
        public async Task<IActionResult> GetActualReservationsByCarIdAsync(int id)
        {
            var result = await service.GetActualReservationsByCarIdAsync(id);
            return Ok(result);
        }

        [HttpGet, Route("users/{id}")]
        public async Task<IActionResult> GetAllReservationsByUserIdAsync(int id)
        {
            var result = await service.GetAllReservationsByUserIdAsync(id);
            return Ok(result);
        }

        [HttpGet, Route("terms/{id}")]
        [HttpGet, Route("terms/{id}/{rentalDate:datetime}/{returnDate:datetime}")]
        public async Task<IActionResult> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate)
        {
            var result = await service.GetFreeTermsByCarIdAsync(id, rentalDate, returnDate);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            if (await service.ReservationCanBeCreatedAsync(reservationCreateDto))
            {
                var result = await service.CreateReservationAsync(reservationCreateDto);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservationAsync(int id, ReservationUpdateDto reservationUpdateDto)
        {
            if (id != reservationUpdateDto.ReservationId)
                return BadRequest();
            if (await service.ReservationCanBeUpdatedAsync(reservationUpdateDto))
            {
                var entity = await service.UpdateReservationAsync(reservationUpdateDto);
                return Ok(entity);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var entity = await service.GetReservationByIdAsync(id);
            try
            {
                if (entity.RentalDate > DateTime.Now)
                {
                    await service.DeleteReservationAsync(id);
                    return Ok();
                }
            }
            catch (NullReferenceException) { return BadRequest(); }
            return BadRequest();
        }
    }
}