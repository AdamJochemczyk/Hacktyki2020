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
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> RegisterDefect(RegisterDefectDto registerDefectDto)
        {
            if (registerDefectDto == null) 
                return BadRequest("Model is empty"); 
            var register_defect = await _defectsService.RegisterDefectAsync(registerDefectDto);
            if (register_defect == null)
                return BadRequest("Object car or user is empty;"); 
            return Ok(register_defect);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDefects()
        {
            var defects = await _defectsService.GetAllDefectsAsync();
            if (defects == null)
                return BadRequest("Database is empty");
            return Ok(defects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDefect(int id)
        {
            var defect = await _defectsService.GetDefectAsync(id);
            if (defect == null)
                return NotFound("This is defect does not exist");
            return Ok(defect);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDefectAsync(UpdateDefectDto updateDefectDto)
        {
          var defect =  await _defectsService.UpdateDefectAsync(updateDefectDto);
            if (defect == null)
                return NotFound("Not found this object");
            return Ok(defect);
        }
    }
}