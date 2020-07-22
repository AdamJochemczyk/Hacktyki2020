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
            try {
                if (createUserDto == null)
                    throw new Exception("Model is empty");
            var new_user = new User(createUserDto.FirstName, createUserDto.LastName, createUserDto.NumberIdentificate,
                createUserDto.Email, createUserDto.MobileNumber);
                var check_user = await _userRepository.FindByLogin(createUserDto.Email);
                if (check_user == null)
                {
                    _userRepository.Create(new_user);
                    await _userRepository.SaveChangesAsync();
                    _email.EmailAfterRegistration(createUserDto);
                    return _mapper.Map<CreateUserDto>(new_user);
                }
               else
                    throw new Exception("User with this email exists");
            }
            catch (InvalidCastException)
            {
                return createUserDto;
            }
        }
        public async Task<CreateUserDto> SetPassword(UpdateUserPasswordDto updateUserDto)
        {
            var user = await _userRepository.FindByIdDetails(updateUserDto.UserId); ;   
            try
            {
                if (updateUserDto.EncodePassword != updateUserDto.ConfirmEncodePassword)
                    throw new Exception("Passwords isn't the same");
                if (user == null)
                    throw new Exception("This user does not exist");
                user.SetPassword(EncodePasswordToBase64(updateUserDto.EncodePassword));
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                user = await _userRepository.FindByIdAsync(updateUserDto.UserId);
                return _mapper.Map<CreateUserDto>(user);
            }catch (InvalidCastException){
                return _mapper.Map<CreateUserDto>(user);
            }
        }
        public async Task<bool> SignIn(UserLoginDto userLoginDto)
        {
            var password = EncodePasswordToBase64(userLoginDto.EncodePassword);
            var user = await _userRepository.FindByLogin(userLoginDto.Email);
            try
            {
                if (user.Email == null)
                    throw new Exception("Email not correct");
                if (user.EncodePassword != password)
                    throw new Exception("Password not correct");
            }
            catch (InvalidCastException) { return false; }
            return true;
        }
    }
}
