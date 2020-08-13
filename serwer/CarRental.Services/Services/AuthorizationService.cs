using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Cryptography;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Token;
using CarRental.Services.Models.User;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{

    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailServices email;
        private readonly IMapper mapper;
        private readonly ITokenGeneratorService token;
        private readonly IRefreshRepository refreshRepository;
        public AuthorizationService(IUserRepository userRepository,
            IEmailServices email,
            IMapper mapper,
            ITokenGeneratorService token,
            IRefreshRepository refreshRepository)
        {
            this.userRepository = userRepository;
            this.email = email;
            this.mapper = mapper;
            this.token = token;
            this.refreshRepository = refreshRepository;
        }

        public static HashSalt GenerateSaltedHash(int size, string password)
        {
            var passwordHasher = new PasswordHasher();
            return passwordHasher.GenerateSaltedHash(size, password);         
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }

        public async Task<CreateUserDto> RegistrationUserAsync(CreateUserDto createUserDto)
        {
            var new_user = new User(createUserDto.FirstName, createUserDto.LastName, createUserDto.NumberIdentificate,
                createUserDto.Email, createUserDto.MobileNumber);
            var check_user = await userRepository.FindByLogin(createUserDto.Email);
            if (check_user == null)
            {
                userRepository.Create(new_user);
                await userRepository.SaveChangesAsync();
                createUserDto.CodeOfVerification = new_user.CodeOfVerification;
                email.EmailAfterRegistration(createUserDto);
            }
            else
                return createUserDto;
            return mapper.Map<CreateUserDto>(new_user);
        }

        public async Task<bool> SetPasswordAsync(UpdateUserPasswordDto updateUserPassword)
        {
            var user = await userRepository.FindByCodeOfVerification(updateUserPassword.CodeOfVerification);
            var saltHashPassword = GenerateSaltedHash(16, updateUserPassword.EncodePassword);
            user.SetPassword(saltHashPassword.Hash, saltHashPassword.Salt);
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<TokenDto> SignInAsync(UserLoginDto userLoginDto)
        {
            var user = await userRepository.FindByLogin(userLoginDto.Email);
            TokenDto tokenDto = new TokenDto();
            if (user == null)
            {
                tokenDto.Code = 401;
                return tokenDto;
            }
            if (userLoginDto.Email != user.Email || !VerifyPassword(userLoginDto.EncodePassword, user.HashPassword, user.Salt))
            {
                tokenDto.Code = 401;
                return tokenDto;
            }
            //Return two tokens Access, Refresh
            tokenDto.Code = 200;
            tokenDto.AccessToken = token.GenerateToken(user);
            tokenDto.RefreshToken = token.RefreshGenerateToken();
            //Save To database Refresh token 
            RefreshToken refreshToken = new RefreshToken(tokenDto.RefreshToken, user.UserId, true);
            refreshRepository.Create(refreshToken);
            await refreshRepository.SaveChangesAsync();
            return tokenDto;
        }
    }
}
