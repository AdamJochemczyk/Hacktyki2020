using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Defect;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class DefectService : IDefectsService
    {
        private readonly IUserRepository userRepository;
        private readonly ICarRepository carRepository;
        private readonly IDefectRepository defectRepository;
        private readonly IMapper mapper;

        public DefectService(IUserRepository userRepository,
            ICarRepository carRepository,
            IMapper mapper,
            IDefectRepository defectRepository) 
        {
            this.userRepository = userRepository;
            this.carRepository = carRepository;
            this.defectRepository = defectRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DefectDto>> GetAllDefectsAsync()
        {
            var defects = await defectRepository.FindAllDefects();
            return mapper.Map<IEnumerable<DefectDto>>(defects);
        }

        public async Task<DefectDto> GetDefectAsync(int Id)
        {
            var defect = await defectRepository.FindDefectById(Id);
            if (defect == null)
                return null;
            return mapper.Map<DefectDto>(defect);
        }

        public async Task<DefectDto> RegisterDefectAsync(RegisterDefectDto registerDefectDto)
        {
            var user = await userRepository.FindByIdDetails(registerDefectDto.UserId);
            var car = await carRepository.FindByIdAsync(registerDefectDto.CarId);
            if (user == null || car == null) { return null; }
            Defect defect = new Defect(user.UserId,
                car.CarId,
                user.FirstName,
                user.LastName,
                car.RegistrationNumber,
                registerDefectDto.Description,
                Status.Reported);

            defectRepository.Create(defect);
            await defectRepository.SaveChangesAsync();
            return mapper.Map<DefectDto>(defect);
        }
        public async Task<DefectDto> UpdateDefectAsync(UpdateDefectDto updateDefectDto)
        {
            var defect = await defectRepository.FindDefectById(updateDefectDto.Id);
            if (defect == null)
                return new DefectDto()
                {
                    Status = updateDefectDto.Status,
                    Description = updateDefectDto.Description,
                    DefectId = updateDefectDto.Id
                };
            defect.Update(updateDefectDto.Description, updateDefectDto.Status);
            await defectRepository.SaveChangesAsync();
            return mapper.Map<DefectDto>(defect);
        }

    }
}
