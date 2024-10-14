﻿using BasketballShopServer.Repositories;
using BasketballShopSharedLibrary.DTOs;
using BasketballShopSharedLibrary.Response;
using Microsoft.AspNetCore.Mvc;

namespace BasketballShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount accountService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse>> CreateAccount(UserDTO model)
        {
            if (model is null) return BadRequest("Model is null");
            var response = await accountService.Register(model);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAccount(LoginDTO model)
        {
            if (model is null) return BadRequest("Model is null");
            var response = await accountService.Login(model);
            return Ok(response);
        }


        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var token = GetTokenFromHeader();
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            var getUser = await accountService.GetUserByToken(token!);
            if (getUser is null || string.IsNullOrEmpty(getUser.Email))
                return Unauthorized();

            return Ok(getUser);
        }


        [HttpPost("refresh_token")]
        public async Task<ActionResult<LoginResponse>> RefreshToken(PostRefreshTokenDTO model)
        {
            if (model is null) return Unauthorized();
            var result = await accountService.GetRefreshToken(model);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<ServiceResponse>> Logout()
        {
            var token = GetTokenFromHeader();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new ServiceResponse(false, "Token is missing."));
            }

            var response = await accountService.Logout(token);
            return Ok(response);
        }
        private string GetTokenFromHeader()
        {
            string Token = string.Empty;
            foreach (var header in Request.Headers)
            {
                if (header.Key.ToString().Equals("Authorization"))
                {
                    Token = header.Value.ToString();
                    break;
                }
            }
            return Token.Split(" ").LastOrDefault()!; 
        }
    } 
}
