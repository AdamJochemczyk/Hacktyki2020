using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Token;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/refresh")]
    [ApiController]
    public class TokensController : Controller
    {
        public ITokenService _tokenService;
        public ITokenGeneratorService _tokenGeneratorService;
        public TokensController(ITokenService tokenService, ITokenGeneratorService tokenGeneratorService)
        {
            _tokenService = tokenService;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(TokenDto refreshToken)
        {
            var refresh = await _tokenService.CheckAccessRefreshToken(refreshToken.RefreshToken);
            if (!refresh.CheckRefreshToken)
                return Unauthorized("Your Refresh Token is bad");
            else
            {
                var tokenRefresh = _tokenGeneratorService.RefreshGenerateToken();
                refreshToken.AccessToken = await _tokenGeneratorService.GenerateToken(refresh.UserId);
                refreshToken.RefreshToken = tokenRefresh;
                _tokenService.SaveRefreshToken(refresh.UserId, refreshToken.RefreshToken, true);
            }
            return Ok(refreshToken);
        }
    }
}