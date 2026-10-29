using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.Features.UserManagement.GetUserByID;
using GloboTicket.Application.Features.UserManagement.GetUsers;
using GloboTicket.Application.Features.UserManagement.SuspendUser;
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

        [HttpGet("")]
        [Authorize(Roles =Roles.SystemAdmin)]
        public async Task<IActionResult> GetUsersAsync(GetUsersQuery query)
        {
           
            var result=(await sender.Send(query)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserByID([FromRoute] GetUserByIDQuery query)
        {
            var result=(await sender.Send(query)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("{userid}/Suspend")]
        [Authorize(Roles =Roles.SystemAdmin)]
        public async Task<IActionResult> SuspendUserAsync([FromRoute]string UserID)
        {
            var result = (await sender.Send(new SuspenionUserQuery(UserID,SuspensionActions.Suspend))).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("{userid}/UnSuspend")]
        [Authorize(Roles =Roles.SystemAdmin)]
        public async Task<IActionResult> UnSuspendUserAsync([FromRoute]string UserID)
        {
            var result = (await sender.Send(new SuspenionUserQuery(UserID,SuspensionActions.UnSuspend))).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }


    }
}
