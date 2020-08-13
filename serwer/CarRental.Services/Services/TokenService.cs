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
        private readonly IRefreshRepository refreshRepository;
        private readonly IUserRepository userRepository;
        private readonly ITokenGeneratorService tokenGeneratorService;
        public TokenService(IRefreshRepository refreshRepository,IUserRepository userRepository,
                                                   ITokenGeneratorService tokenGeneratorService)
        {
            this.refreshRepository = refreshRepository;
            this.userRepository = userRepository;
            this.tokenGeneratorService = tokenGeneratorService;
        }
        public async Task<TokenClaimsDto> CheckAccessRefreshTokenAsync(string refresh)
        {
            TokenClaimsDto token = new TokenClaimsDto();
            var check = await refreshRepository.FindByRefreshToken(refresh);
            if (check == null || check.DateOfEnd < DateTime.Now)
            {
                token.CheckRefreshToken = false;
                return token;
            }
            check.Delete(false);
            await refreshRepository.SaveChangesAsync();
            token.UserId = check.UserId;
            token.CheckRefreshToken = true;
            return token;

        }
        public async Task<TokenDto> GenerateRefreshTokenAsync(TokenClaimsDto token)
        {
            TokenDto tokenDto = new TokenDto();
            var user = await userRepository.FindByIdDetails(token.UserId);
            tokenDto.AccessToken = tokenGeneratorService.GenerateToken(user);
            tokenDto.RefreshToken = tokenGeneratorService.RefreshGenerateToken();
            tokenDto.Code = 200;
            return tokenDto;
        }
        public async Task<TokenDto> SaveRefreshTokenAsync(int id, string refreshtoken, bool isvalid)
        {
            RefreshToken refresh = new RefreshToken(refreshtoken, id, isvalid);
            refreshRepository.Create(refresh);
           await refreshRepository.SaveChangesAsync();
            return new TokenDto() { RefreshToken = refresh.Refresh };
        }
    }
}
