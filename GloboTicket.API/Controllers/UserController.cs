using GloboTicket.Application.Features.UserManagement.GetUsers;
using GloboTicket.Infrastructure.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GloboTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public ISender  sender { get; set; }

        public UserController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("users")]
        [Authorize(Roles =Roles.SystemAdmin)]
        public async Task<IActionResult> UsersAsync(GetUsersQuery query)
        {
           
            var result=(await sender.Send(query)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }

    }
}
