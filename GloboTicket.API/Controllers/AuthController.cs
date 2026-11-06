using GloboTicket.Application.DTOS.Authentitcation.Login;
using GloboTicket.Application.Features.Authentication.CustomerRegister;
using GloboTicket.Application.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace GloboTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender sender;

        public AuthController(ISender sender)
        {
            this.sender = sender;
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login()
        //{

        //}
        [HttpPost("Register")]
        public async Task<IActionResult> Register(CustomerRegisterCommand register,CancellationToken cancellationToken)
        {
            var Result=(await sender.Send(register,cancellationToken)).ToOperationResult();
            return StatusCode(Result.StatusCode, Result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            var Result = (await sender.Send(loginCommand, cancellationToken)).ToOperationResult();
            if (!Result.IsSuccess)
                return StatusCode(Result.StatusCode, Result);


            Response.Cookies.Append("Refresh", Result.Data!.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure=true,
                    SameSite=SameSiteMode.None,
                    Expires=DateTimeOffset.UtcNow.AddHours(2),
                    Path = "/api/Auth/refresh"

            });
                var res = Result.Data;
                var loginResponse = new LoginResponseDTO
                {
                    Uid = res.Uid,
                    AccessToken = res.AccessToken,
                    Roles = res.Roles,
                    RefreshTokenExpirationDate = res.RefreshTokenExpirationDate
                };
                return Ok(loginResponse);
            

           
        }
    }
}
