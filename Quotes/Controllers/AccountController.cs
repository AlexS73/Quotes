using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes.Core.HelpModel;
using Quotes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = await accountService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Логин или пароль не корректны." });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest model)
        {
            var response = await accountService.Registration(model);

            if (response == null)
                return BadRequest(new { message = "Логин или пароль не корректны." });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await accountService.RefreshToken(refreshToken);

            if (response == null)
                return ValidationProblem("Invalid token");

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }


        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model?.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            bool response = accountService.RevokeToken(token);

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        private void setTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
