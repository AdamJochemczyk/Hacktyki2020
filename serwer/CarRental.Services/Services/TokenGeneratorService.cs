using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public TokenGeneratorService() { }
        public string GenerateToken(User _user)
        {
            var claims = new List<Claim> {
                     new Claim(ClaimTypes.Email,_user.Email),
                     new Claim(ClaimTypes.Hash , _user.HashPassword),
                     new Claim(ClaimTypes.Role,_user.RoleOfUser.ToString()),
                     new Claim(JwtRegisteredClaimNames.Sub,_user.UserId.ToString())
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
