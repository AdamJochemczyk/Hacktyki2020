using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
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
        //For decode password @Zaneta
        //public string DecodeFrom64(string encodeddata)
        //{
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    System.Text.Decoder utf8decode = encoder.GetDecoder();
        //    byte[] todecode_byte = Convert.FromBase64String(encodeddata);
        //    int charcount = utf8decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        //    char[] decoded_char = new char[charcount];
        //    utf8decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        //    string result = new string(decoded_char);
        //    return result;
        //}
        public async Task DeleteUser(int Id) 
        {
            
            var user = await _userRepository.FindByIdAsync(Id);
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsersDto>> GetAllUsers()
        {
           var all_users = await _userRepository.FindAllAsync();
            return _mapper.Map<IEnumerable<UsersDto>>(all_users);
        }

        public async Task<UsersDto> GetUser(int Id)
        {
            var user = await _userRepository.FindByIdAsync(Id);
            return _mapper.Map<UsersDto>(user);
        }

        public async Task<UsersDto> UpdateUser(UsersDto usersDto)
        {
         
                var user = await _userRepository.FindByIdAsync(usersDto.UserId);
                var check_user = await _userRepository.FindByLogin(usersDto.Email);
                if (check_user.Email == user.Email)
                {
                    user.Update(usersDto.FirstName, usersDto.LastName, usersDto.NumberIdentificate, usersDto.Email, usersDto.MobileNumber);
                    _userRepository.Update(user);
                    await _userRepository.SaveChangesAsync();
                }
                else
                return _mapper.Map<UsersDto>(user);
                  
                user = await _userRepository.FindByIdAsync(usersDto.UserId);
            return _mapper.Map<UsersDto>(user);

        }


    }
}
