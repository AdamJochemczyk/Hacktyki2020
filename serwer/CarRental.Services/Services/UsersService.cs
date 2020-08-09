using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersService(
          IUserRepository userRepository,
          IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user == null) { return false; }
            user.Delete(true);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UsersDto>> GetAllUsers()
        {
            var all_users = await _userRepository.FindAllUsers();
            return _mapper.Map<IEnumerable<UsersDto>>(all_users);
        }

        public async Task<UsersDto> GetUser(int id)
        {
            var user = await _userRepository.FindByIdDetails(id);
            return _mapper.Map<UsersDto>(user);
        }

        public async Task<UsersDto> UpdateUser(UsersDto usersDto)
        {
            var user = await _userRepository.FindByIdDetails(usersDto.UserId);
            if (user == null)
            {
                usersDto.isValid = false;
                return usersDto;
            }
            if (usersDto.Email == user.Email)
            {
                usersDto.isValid = true;
                user.Update(usersDto.FirstName, usersDto.LastName, usersDto.NumberIdentificate, usersDto.Email, usersDto.MobileNumber);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                return usersDto;
            }
            else
                return _mapper.Map<UsersDto>(user);
        }


    }
}
