using CarRental.API.Attributes;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : Controller
    {
        private readonly IReservationService reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var reservations = await reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var reservation = await reservationService.GetReservationByIdAsync(id);
            return Ok(reservation);
        }

        [HttpGet, Route("cars/{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetActualReservationsByCarIdAsync(int id)
        {
            var reservations = await reservationService.GetActualReservationsByCarIdAsync(id);
            return Ok(reservations);
        }

        [HttpGet, Route("users/{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetAllReservationsByUserIdAsync(int id)
        {
            var reservations = await reservationService.GetAllReservationsByUserIdAsync(id);
            return Ok(reservations);
        }

        [HttpPost]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            if (await reservationService.ReservationCanBeCreatedAsync(reservationCreateDto))
            {
                var reservation = await reservationService.CreateReservationAsync(reservationCreateDto);
                return Ok(reservation);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> UpdateReservationAsync(int id, ReservationUpdateDto reservationUpdateDto)
        {
            if (id != reservationUpdateDto.ReservationId)
            {
                return BadRequest();
            }
            if (await reservationService.ReservationCanBeUpdatedAsync(reservationUpdateDto))
            {
                var reservation = await reservationService.UpdateReservationAsync(reservationUpdateDto);
                return Ok(reservation);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var reservation = await reservationService.GetReservationByIdAsync(id);
            if (reservation != null)
            {
                await reservationService.DeleteReservationAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}