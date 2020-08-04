using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/terms")]
    [ApiController]
    public class TermsController : Controller
    {
        private readonly ITermService service;
        public TermsController(ITermService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        [HttpGet("{id}/{rentalDate:datetime}/{returnDate:datetime}")]
        public async Task<IActionResult> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate)
        {
            var result = await service.GetFreeTermsByCarIdAsync(id, rentalDate, returnDate);
            return Ok(result);
        }
    }
}