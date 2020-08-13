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
        private readonly IUserRepository _userRepository;
        private readonly IEmailServices _email;
        private readonly IMapper _mapper;
        private readonly ITokenGeneratorService _token;
        private readonly IRefreshRepository _refreshRepository;
        public AuthorizationService(IUserRepository userRepository,
            IEmailServices email,
            IMapper mapper,
            ITokenGeneratorService token,
            IRefreshRepository refreshRepository)
        {
            _userRepository = userRepository;
            _email = email;
            _mapper = mapper;
            _token = token;
            _refreshRepository = refreshRepository;
        }

        public static HashSalt GenerateSaltedHash(int size, string password)
        {
            var passwordHasher = new PasswordHasher();
            return passwordHasher.GenerateSaltedHash(size, password);
            //var saltBytes = new byte[size];
            //var provider = new RNGCryptoServiceProvider();
            //provider.GetNonZeroBytes(saltBytes);
            //var salt = Convert.ToBase64String(saltBytes);

            //var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            //var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            //HashSaltDto hashSalt = new HashSaltDto { Hash = hashPassword, Salt = salt };
            //return hashSalt;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }

        public async Task<CreateUserDto> RegistrationUserAsync(CreateUserDto createUserDto)
        {
            var new_user = new User(createUserDto.FirstName, createUserDto.LastName, createUserDto.IdentificationNumber,
                createUserDto.Email, createUserDto.MobileNumber);
            var check_user = await _userRepository.FindByLogin(createUserDto.Email);
            if (check_user == null)
            {
                _userRepository.Create(new_user);
                await _userRepository.SaveChangesAsync();
                createUserDto.CodeOfVerification = new_user.CodeOfVerification;
                _email.EmailAfterRegistration(createUserDto);
            }
            else
                return createUserDto;
            return _mapper.Map<CreateUserDto>(new_user);
        }

        public async Task<bool> SetPassword(UpdateUserPasswordDto updateUserPassword)
        {
            var user = await _userRepository.FindByCodeOfVerification(updateUserPassword.CodeOfVerification);
            var saltHashPassword = GenerateSaltedHash(16, updateUserPassword.EncodePassword);
            user.SetPassword(saltHashPassword.Hash, saltHashPassword.Salt);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<TokenDto> SignIn(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.FindByLogin(userLoginDto.Email);
            if (user == null)
            {
                TokenDto tokenError = new TokenDto();
                tokenError.Code = 401;
                return tokenError;
            }
            if (userLoginDto.Email != user.Email || !VerifyPassword(userLoginDto.Password, user.HashPassword, user.Salt))
            {
                TokenDto tokenError = new TokenDto();
                tokenError.Code = 401;
                return tokenError;
            }
            //Return two tokens Access, Refresh
            TokenDto token = new TokenDto();
            token.Code = 200;
            token.AccessToken = _token.GenerateToken(user);
            token.RefreshToken = _token.RefreshGenerateToken();
            //Save To database Refresh token 
            RefreshToken refreshToken = new RefreshToken(token.RefreshToken, user.UserId, true);
            _refreshRepository.Create(refreshToken);
            await _refreshRepository.SaveChangesAsync();
            return token;
        }
    }
}
