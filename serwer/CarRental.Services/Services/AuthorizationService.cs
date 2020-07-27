﻿using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public string DecodeFrom64(string encodeddata)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodeddata);
            int charcount = utf8decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charcount];
            utf8decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new string(decoded_char);
            return result;
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
                var check_user = await _userRepository.FindByLogin(createUserDto.Email);
            if (check_user == null)
            {
                _userRepository.Create(new_user);
                await _userRepository.SaveChangesAsync();
                createUserDto.UserId = new_user.UserId;
                createUserDto.CodeOfVerification = new_user.CodeOfVerification;
                _email.EmailAfterRegistration(createUserDto);
            }
            else
                return createUserDto;
                
            return _mapper.Map<CreateUserDto>(new_user);


        }




        public async Task<bool> SetPassword(UpdateUserPasswordDto updateUserPassword)
        {
            //Decode token
            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(updateUserPassword.Token);

            //Find by id and update password
            var user = await _userRepository.FindByCodeOfVerification(updateUserPassword.CodeOfVerification);
            user.SetPassword(EncodePasswordToBase64(updateUserPassword.EncodePassword));
            _userRepository.Update(user);
           await _userRepository.SaveChangesAsync();
            return true;
        }
     
        public async Task<string> SignIn(UserLoginDto userLoginDto)
        {
            var password =EncodePasswordToBase64(userLoginDto.EncodePassword);
            var user = await _userRepository.FindByLogin(userLoginDto.Email);
            if (user.Email != userLoginDto.Email)
                return "Email is not correct";
            else if (user.EncodePassword != password)
                return "Password is not correct";

            var claims = new List<Claim> {
                     new Claim(JwtRegisteredClaimNames.Email,userLoginDto.Email),
                     new Claim(JwtRegisteredClaimNames.Sub , userLoginDto.EncodePassword),
                     new Claim(JwtRegisteredClaimNames.Jti,userLoginDto.RoleOfWorker.ToString())
            };
          
            var jwt = new JwtSecurityToken(
                  issuer: TokenOptions.ISSUER,
                  audience: TokenOptions.AUDIENCE,
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(TokenOptions.LIFETIME),
                  signingCredentials: new SigningCredentials(TokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            

            return encodedJwt;
        }
    }
}
