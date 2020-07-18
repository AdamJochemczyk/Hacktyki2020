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

    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailServices _email;
        private readonly IMapper _mapper;
        public AuthorizationService(IUserRepository userRepository,IEmailServices email , IMapper mapper)
        {
            _userRepository = userRepository;
            _email = email;
            _mapper = mapper;
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
        public async Task<CreateUserDto> RegistrationUserAsync(CreateUserDto createUserDto)
        {
            var new_user = new User(createUserDto.FirstName, createUserDto.LastName, createUserDto.NumberIdentificate,
                createUserDto.Email, createUserDto.MobileNumber);
            _userRepository.Create(new_user);
            await _userRepository.SaveChangesAsync();
            new_user = await _userRepository.FindByIdDetails(new_user.UserId);
            createUserDto.UserId = new_user.UserId;
            _email.EmailAfterRegistration(createUserDto);
            return _mapper.Map<CreateUserDto>(new_user);
        }
        public async Task<CreateUserDto> SetPassword(UpdateUserPasswordDto updateUserDto)
        {
            var user = await _userRepository.FindByIdDetails(updateUserDto.UserId);
            user.SetPassword(EncodePasswordToBase64(updateUserDto.EncodePassword));
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            user = await _userRepository.FindByIdAsync(updateUserDto.UserId);
            return _mapper.Map<CreateUserDto>(user);
        }
        public async Task<bool> SignIn(UserLoginDto userLoginDto)
        {
            userLoginDto.EncodePassword = EncodePasswordToBase64(userLoginDto.EncodePassword);
            var user = await _userRepository.VerificateUser(userLoginDto.Email, userLoginDto.EncodePassword);
            if (user != null)
                return true;

            return false;
        }
    }
}
