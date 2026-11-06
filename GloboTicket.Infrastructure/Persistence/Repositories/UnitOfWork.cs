using GloboTicket.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction=await dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
           if(_transaction is null)
                throw new InvalidOperationException("No transaction in progress.");
           await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null)
                throw new InvalidOperationException("No transaction in progress.");

            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            await dbContext.SaveChangesAsync(token);
        }
    }
}
