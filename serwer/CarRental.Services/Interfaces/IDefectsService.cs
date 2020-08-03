using CarRental.Services.Models.Defect;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
   public interface IDefectsService
    {
        public Task<DefectDto> RegisterDefectAsync(RegisterDefectDto registerDefectDto);
        public Task<IEnumerable<DefectDto>> GetAllDefects();
        public Task<DefectDto> GetDefect(int Id);
    }
}
