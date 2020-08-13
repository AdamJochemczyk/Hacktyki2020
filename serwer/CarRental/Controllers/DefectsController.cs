using System.Resources;
using System.Threading.Tasks;
using CarRental.API.Resources;
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
        private readonly IDefectsService defectsService;
        public ResourceManager resourcesManager;
        public DefectsController(IDefectsService defectsService)
        {
            this.defectsService = defectsService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Worker")]
        public async Task<IActionResult> RegisterDefectAsync(RegisterDefectDto registerDefectDto)
        {
            if (registerDefectDto == null)
            {
                return BadRequest(resourcesManager.GetString("Model"));
            }
            var register_defect = await defectsService.RegisterDefectAsync(registerDefectDto);
            if (register_defect == null)
            {
                return BadRequest("ObjectCarUser");
            }
            return Ok(register_defect);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDefectsAsync()
        {
            var defects = await defectsService.GetAllDefectsAsync();
            if (defects == null)
            {
                return BadRequest(resourcesManager.GetString("DatabaseEmpty"));
            }
            return Ok(defects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDefectAsync(int id)
        {
            var defect = await defectsService.GetDefectAsync(id);
            if (defect == null)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            return Ok(defect);
        }
        [HttpPut("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDefectAsync(int id, UpdateDefectDto updateDefectDto)
        {
            if (id != updateDefectDto.Id)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            var defect = await defectsService.UpdateDefectAsync(updateDefectDto);
            if (defect == null)
            {
                return NotFound(resourcesManager.GetString("NotFound"));
            }
            return Ok(defect);
        }
    }
}