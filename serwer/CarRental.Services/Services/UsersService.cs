using AutoMapper;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UsersService(
          IUserRepository userRepository,
          IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await userRepository.FindByIdAsync(id);
            if (user == null) { return false; }
            user.Delete(true);
            await userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UsersDto>> GetAllUsersAsync()
        {
            var all_users = await userRepository.FindAllUsers();
            return mapper.Map<IEnumerable<UsersDto>>(all_users);
        }

        public async Task<UsersDto> GetUserAsync(int id)
        {
            var user = await userRepository.FindByIdDetails(id);
            return mapper.Map<UsersDto>(user);
        }

        public async Task<UsersDto> UpdateUserAsync(UsersDto usersDto)
        {
            var user = await userRepository.FindByIdDetails(usersDto.UserId);
            if (user == null)
            {
                usersDto.isValid = false;
                return usersDto;
            }
            if (usersDto.Email == user.Email)
            {
                usersDto.isValid = true;
                user.Update(usersDto.FirstName, usersDto.LastName, usersDto.IdentificationNumber, usersDto.Email, usersDto.MobileNumber);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                return usersDto;
            }
            else
                return mapper.Map<UsersDto>(user);
        }
    }
}
