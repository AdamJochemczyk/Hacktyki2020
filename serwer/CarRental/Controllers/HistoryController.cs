using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : Controller
    {
        private readonly IReservationService reservationService;
        public HistoryController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllReservationsByUserId(int id)
        {
            var result = await reservationService.GetAllReservationsByUserIdAsync(id);
            return Ok(result); 
        }
    }
}