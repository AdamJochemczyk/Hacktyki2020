using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Defect;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class DefectService : IDefectsService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        private readonly IDefectRepository _defectRepository;
        private readonly IMapper _mapper;
        public DefectService(IUserRepository userRepository, ICarRepository carRepository, IMapper mapper,
                                                                     IDefectRepository defectRepository)
        {
            _userRepository = userRepository;
            _carRepository = carRepository;
            _defectRepository = defectRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DefectDto>> GetAllDefectsAsync()
        {
            var defects = await _defectRepository.FindAllAsync();
            if (defects == null) { return null; }
            return _mapper.Map<IEnumerable<DefectDto>>(defects);
        }

        public async Task<DefectDto> GetDefectAsync(int Id)
        {
            var defect = await _defectRepository.FindDefectById(Id);
            if (defect == null)
                return null;
            return _mapper.Map<DefectDto>(defect);
        }

        public async Task<DefectDto> RegisterDefectAsync(RegisterDefectDto registerDefectDto)
        {

            var user = await _userRepository.FindByIdDetails(registerDefectDto.UserId);
            var car = await _carRepository.FindByIdAsync(registerDefectDto.CarId);
            if (user == null || car == null) { return null; }
            Defect defect = new Defect(user.UserId, car.CarId, user.FirstName, user.LastName, car.RegistrationNumber,
                                                    registerDefectDto.Description, Status.Reported);
            _defectRepository.Create(defect);
            await _defectRepository.SaveChangesAsync();

            return _mapper.Map<DefectDto>(defect);
        }
        public async Task<DefectDto> UpdateDefectAsync(UpdateDefectDto updateDefectDto)
        {
            var defect = await _defectRepository.FindDefectById(updateDefectDto.Id);
            if (defect == null)
                return _mapper.Map<DefectDto>(defect);
            defect.Update(updateDefectDto.Description, updateDefectDto.Status);
            await _defectRepository.SaveChangesAsync();

            return _mapper.Map<DefectDto>(defect);
        }

    }
}
