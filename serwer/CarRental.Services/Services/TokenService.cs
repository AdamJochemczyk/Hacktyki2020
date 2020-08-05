using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Token;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRefreshRepository _refreshRepository;
        private readonly IUserRepository _userRepository;
        public TokenService(IRefreshRepository refreshRepository,IUserRepository userRepository)
        {
            _refreshRepository = refreshRepository;
            _userRepository = userRepository;
        }
        public async Task<string> GenerateToken(int UserId)
        {
            var user =  await _userRepository.FindByIdDetails(UserId);
            var claims = new List<Claim> {
                     new Claim(JwtRegisteredClaimNames.Email,user.Email),
                     new Claim(JwtRegisteredClaimNames.Sub , user.HashPassword),
                     new Claim(JwtRegisteredClaimNames.Jti,user.RoleOfUser.ToString())
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

        public async Task<TokenClaimsDto> CheckAccessRefreshToken(string refresh)
        {
            TokenClaimsDto token = new TokenClaimsDto();
            var check = await _refreshRepository.FindByRefreshToken(refresh);
            if (check==null||check.DateOfEnd < DateTime.Now)
            {                
                token.CheckRefreshToken = false;
                return token;
            }
            check.Delete(false);
            await _refreshRepository.SaveChangesAsync();
            token.UserId = check.UserId;
            token.CheckRefreshToken = true;
            return token;
                
        }

        public void SaveRefreshToken(int id,string refreshtoken,bool isvalid)
        {
            RefreshToken refresh = new RefreshToken(refreshtoken, id, isvalid );
            _refreshRepository.Create(refresh);
            _refreshRepository.SaveChangesAsync();
        }

        public string RefreshGenerateToken()
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[128];
                rng.GetBytes(tokenData);

                var refresh = Convert.ToBase64String(tokenData);
                return refresh;
            }

        }

       
    }
}
