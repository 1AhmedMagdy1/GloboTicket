using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Identity.Authorization
{
    public interface IRequireAuthorization
    {
        public  IReadOnlyCollection<string> RequiredRoles { get; }
       

    }
}
