using FinanceTracker.DAL.EfCore.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL.EfCore.Repositories;

public class BaseRepository
{
    protected BaseRepository(DbContext context)
    {
        DataContext = context;
    }

    protected readonly DbContext DataContext;

    protected void PrepareForCreation(IHasCreationTime entity)
    {
        if(entity is null)
            throw new ArgumentNullException(nameof(entity));

        entity.CreatedTime = DateTime.UtcNow;
    }

    protected void UpdateEntity(IHasModificationTime entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (DataContext.Entry(entity).State == EntityState.Detached)
        {
            DataContext.Attach(entity);
            DataContext.Update(entity);
        }

        entity.ModificationTime = DateTime.UtcNow;
    }

    protected void DeleteEntity(ISoftDelete entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (DataContext.Entry(entity).State == EntityState.Detached)
        {
            DataContext.Attach(entity);
            DataContext.Update(entity);
        }

        entity.IsDeleted = true;
        entity.DeletionTime = DateTime.UtcNow;
    }
}
