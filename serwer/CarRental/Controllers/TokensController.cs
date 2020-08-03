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
        public  ITokenService _tokenService;
        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost]
       public async Task<IActionResult> RefreshToken(TokenDto refreshToken)
        {
            var refresh = await _tokenService.CheckAccessRefreshToken(refreshToken.RefreshToken);
            if (!refresh.CheckRefreshToken)
                return BadRequest("Your Refresh Token is bad");
            else
            {
                var tokenRefresh = _tokenService.RefreshGenerateToken();
                refreshToken.AccessToken = await _tokenService.GenerateToken(refresh.UserId);
                refreshToken.RefreshToken = tokenRefresh;
                _tokenService.SaveRefreshToken(refresh.UserId, refreshToken.RefreshToken, true);
            }
            return Ok(refreshToken);

        }

    }
}