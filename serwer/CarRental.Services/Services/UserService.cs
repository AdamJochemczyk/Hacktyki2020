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
        private readonly IEmailServices _email;
        public UsersService(
          IUserRepository userRepository,
          IMapper mapper,IEmailServices email)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _email = email;
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //For decode password @Zaneta
        //public string DecodeFrom64(string encodedData)
        //{
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //    byte[] todecode_byte = Convert.FromBase64String(encodedData);
        //    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        //    char[] decoded_char = new char[charCount];
        //    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        //    string result = new String(decoded_char);
        //    return result;
        //}

        public async Task<CreateUserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var new_user = new User(createUserDto.FirstName, createUserDto.LastName ,createUserDto.NumberIdentificate,
                createUserDto.Email,createUserDto.MobileNumber,EncodePasswordToBase64(createUserDto.EncodePassword));
            _userRepository.Create(new_user);
            // await _userRepository.SaveChangesAsync();
            _email.EmailAfterRegistration(createUserDto);
            new_user = await _userRepository.FindByIdDetails(new_user.UserId);
            return _mapper.Map<CreateUserDto>(new_user);

        }

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
            var reverse_map = _mapper.Map<User>(usersDto);
            user.Update(reverse_map);
             _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            user = await _userRepository.FindByIdAsync(usersDto.UserId);
            return _mapper.Map<UsersDto>(user);
        }


        //public Task<CreateUserDto> CreateReservationAsync(CreateUserDto createUserDto)
        //{
        //    var new_user = new User(createUserDto.FirstName,createUserDto.LastName)
        //}
    }
}
