using System;
using System.Resources;
using System.Threading.Tasks;
using CarRental.API.Resources;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate)
        {
            if (!termService.DatesHaveValue(rentalDate, returnDate) || termService.DatesAreCorrect(rentalDate.Value, returnDate.Value))
            {
                var terms = await termService.GetFreeTermsByCarIdAsync(id, rentalDate, returnDate);
                return Ok(terms);
            }
            return BadRequest(resourcesManager.GetString("BadDateOrder"));
        }

        //example: .../api/terms/2021-08-07/2021-09-07
        [HttpGet, Route("{rentalDate:datetime}/{returnDate:datetime}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetAvailableCars(DateTime rentalDate, DateTime returnDate)
        {
            if (termService.DatesAreCorrect(rentalDate, returnDate))
            {
                var cars = await carService.GetAvailableCars(rentalDate, returnDate);
                return Ok(cars);
            }
            return BadRequest(resourcesManager.GetString("BadDateOrder"));
        }
    }
}