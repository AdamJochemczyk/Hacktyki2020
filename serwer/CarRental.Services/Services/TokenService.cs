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
        private readonly IUserRepository _userRepository;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        public TokenService(IRefreshRepository refreshRepository,IUserRepository userRepository,
                                                   ITokenGeneratorService tokenGeneratorService)
        {
            _refreshRepository = refreshRepository;
            _userRepository = userRepository;
            _tokenGeneratorService = tokenGeneratorService;
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
        public async Task<TokenDto> GenerateRefreshToken(TokenClaimsDto token)
        {
            TokenDto tokenDto = new TokenDto();
            var user = await _userRepository.FindByIdDetails(token.UserId);
            tokenDto.AccessToken = _tokenGeneratorService.GenerateToken(user);
            tokenDto.RefreshToken = _tokenGeneratorService.RefreshGenerateToken();
            tokenDto.Code = 200;
            return tokenDto;
        }
        public async Task<TokenDto> SaveRefreshToken(int id, string refreshtoken, bool isvalid)
        {
            RefreshToken refresh = new RefreshToken(refreshtoken, id, isvalid);
            _refreshRepository.Create(refresh);
           await _refreshRepository.SaveChangesAsync();
            return new TokenDto() { RefreshToken = refresh.Refresh };
        }
    }
}
