using CarRental.Services.Models.Token;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
  public  interface ITokenService
    {
        public Task<string> GenerateToken(int userId);
        public string RefreshGenerateToken();
        public Task<TokenClaimsDto> CheckAccessRefreshToken(string refresh);
        public void SaveRefreshToken(int id, string refreshtoken, bool isvalid);

    }
}
