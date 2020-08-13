using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var result = await service.GetAllReservationsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await service.GetReservationByIdAsync(id);
            return Ok(result);
        }

        [HttpGet, Route("cars/{id}")]
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> GetActualReservationsByCarIdAsync(int id)
        {
            var result = await service.GetActualReservationsByCarIdAsync(id);
            return Ok(result);
        }

        [HttpGet, Route("users/{id}")]
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> GetAllReservationsByUserIdAsync(int id)
        {
            var result = await service.GetAllReservationsByUserIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Worker")]
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
        [Authorize(Roles = "Admin, Worker")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var entity = await service.GetReservationByIdAsync(id);
            if (entity != null)
            {
                await service.DeleteReservationAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}