using System;
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
        public async Task<IActionResult> GetReservationByIdAsync(int id)
        {
            var result = await service.GetReservationByIdAsync(id);
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
                await service.UpdateReservationAsync(reservationUpdateDto);
                var entity = await service.GetReservationByIdAsync(id);
                return Ok(entity);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var entity = await service.GetReservationByIdAsync(id);
            if (entity.RentalDate > DateTime.Now)
            {
                await service.DeleteReservationAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}