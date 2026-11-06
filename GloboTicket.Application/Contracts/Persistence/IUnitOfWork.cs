using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken);
        public Task BeginTransactionAsync(CancellationToken cancellationToken);
        public Task CommitTransactionAsync(CancellationToken cancellationToken);
        public Task RollbackTransactionAsync(CancellationToken cancellationToken);
    }
}
