using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Defect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/defects")]
    [ApiController]
    public class DefectsController : Controller
    {
        private readonly IDefectsService _defectsService;
        public DefectsController(IDefectsService defectsService)
        {
            _defectsService = defectsService;
        }

        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> RegisterDefect(RegisterDefectDto registerDefectDto)
        {
            if (registerDefectDto == null) { return BadRequest("Model is empty");}
          var register_defect =  await _defectsService.RegisterDefectAsync(registerDefectDto);
            if (register_defect == null) { return BadRequest("Object car or user is empty;"); }
            return Ok(register_defect);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDefects()
        {
            var defects = await _defectsService.GetAllDefects();
            if (defects == null)
                return BadRequest("Database is empty");
            return Ok(defects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDefect(int id)
        {
            var defect = await _defectsService.GetDefect(id);
            if (defect == null)
                return BadRequest("This is defect does not exist");
            return Ok(defect);
        }
         //[HttpPut]
         //public async Task<IActionResult> UpdateDefect()
         // {

         // }
    }
}