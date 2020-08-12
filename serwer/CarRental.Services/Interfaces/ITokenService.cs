using CarRental.Services.Models.Token;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenClaimsDto> CheckAccessRefreshToken(string refresh);
        Task<TokenDto> GenerateRefreshToken(TokenClaimsDto token);
        Task<TokenDto> SaveRefreshToken(int id, string refreshtoken, bool isvalid);

    }
}
