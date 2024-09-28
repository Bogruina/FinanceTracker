using FinanceTracker.DAL.EfCore.Interfaces.Context;
using FinanceTracker.DAL.EfCore.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinanceTracker.DAL.EfCore.Context;

public class ContextFacade<TContext>: IDataContext, IUnitOfWork, IDisposable where TContext : DbContext
{
    private readonly TContext _context;

    private bool _disposed;
    private IDbContextTransaction _transaction;
    public ContextFacade(TContext context)
    {
        _context = context;
    }

    public void CreateTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void RollbackTransaction()
    {
        _transaction.Rollback();
        _transaction.Dispose();
    }

    public void CommitTransaction()
    {
        _transaction.Commit();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            return;
        }

        if (disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
}
