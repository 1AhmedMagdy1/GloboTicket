using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.DTOS.UserManagement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.GetUsers
{
    public sealed record  GetUsersQuery:IRequest<Result<Pagination<UserInfo>>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? Search { get; set; }//search by email or user name
        public bool? IsSuspended { get; set; }
        public List<SystemRolesEnum>? Roles { get; set; }
    }
}
