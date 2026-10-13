using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
    }
}
