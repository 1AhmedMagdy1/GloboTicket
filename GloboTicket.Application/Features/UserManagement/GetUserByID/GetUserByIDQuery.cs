using GloboTicket.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using GloboTicket.Application.DTOS.UserManagement;
namespace GloboTicket.Application.Features.UserManagement.GetUserByID
{
    public class GetUserByIDQuery:IRequest<Result<UserInfo>>
    {
        public string ID { get; set; } = string.Empty;
    }
}
