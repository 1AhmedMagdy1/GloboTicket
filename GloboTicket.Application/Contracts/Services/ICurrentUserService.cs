using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Services
{
    public interface ICurrentUserService
    {
        public string? getCurrentUserId();
    }
}
