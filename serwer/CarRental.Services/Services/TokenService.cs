using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Token;
using System;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRefreshRepository _refreshRepository;
        public TokenService(IRefreshRepository refreshRepository)
        {
            _refreshRepository = refreshRepository;
        }
        public async Task<TokenClaimsDto> CheckAccessRefreshToken(string refresh)
        {
            TokenClaimsDto token = new TokenClaimsDto();
            var check = await _refreshRepository.FindByRefreshToken(refresh);
            if (check == null || check.DateOfEnd < DateTime.Now)
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

        public void SaveRefreshToken(int id, string refreshtoken, bool isvalid)
        {
            RefreshToken refresh = new RefreshToken(refreshtoken, id, isvalid);
            _refreshRepository.Create(refresh);
            _refreshRepository.SaveChangesAsync();
        }
    }
}
