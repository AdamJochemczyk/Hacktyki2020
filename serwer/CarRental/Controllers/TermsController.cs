using System;
using System.Resources;
using System.Threading.Tasks;
using CarRental.API.Resources;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/terms")]
    [ApiController]
    public class TermsController : Controller
    {
        private readonly ITermService termService;
        private readonly ICarService carService;
        public ResourceManager resourcesManager;
        public TermsController(ITermService termService, ICarService carService)
        {
            this.termService = termService;
            this.carService = carService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }

        [HttpGet("{id}")]
        [HttpGet("{id}/{rentalDate:datetime}/{returnDate:datetime}")]
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate)
        {
            if (!DatesHaveValue(rentalDate, returnDate) || DatesAreCorrect(rentalDate.Value, returnDate.Value))
            {
                var result = await termService.GetFreeTermsByCarIdAsync(id, rentalDate, returnDate);
                return Ok(result);
            }
            return BadRequest(resourcesManager.GetString("BadDateOrder"));
        }

        //example: .../api/terms/2021-08-07/2021-09-07
        [HttpGet, Route("{rentalDate:datetime}/{returnDate:datetime}")]
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> GetAvailableCars(DateTime rentalDate, DateTime returnDate)
        {
            if (DatesAreCorrect(rentalDate, returnDate))
            {
                var entities = await carService.GetAvailableCars(rentalDate, returnDate);
                return Ok(entities);
            }
            return BadRequest(resourcesManager.GetString("BadDateOrder"));
        }

        private bool DatesAreCorrect(DateTime rentalDate, DateTime returnDate)
        {
            return rentalDate < returnDate && rentalDate.Date >= DateTime.Now.Date;
        }

        private bool DatesHaveValue(DateTime? rentalDate, DateTime? returnDate)
        {
            return rentalDate.HasValue && returnDate.HasValue;
        }
    }
}