using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS.UserManagement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.UpdateUser
{
    public sealed record UpdateUserCommand:IRequest<Result<bool>>
    {
        public UpdateUserCommand(string userID,UpdateUserDto userDto)
        {
            UserID = userID;
            FirstName = userDto.FirstName;
            LastName = userDto.LastName;
            
        }
        public string UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
      

    }
}
