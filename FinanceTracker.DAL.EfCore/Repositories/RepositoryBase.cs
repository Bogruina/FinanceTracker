using FinanceTracker.DAL.EfCore.Context;
using FinanceTracker.DAL.EfCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinanceTracker.DAL.EfCore.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly ApplicationContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(ICollection<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<int> Count()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<List<TEntity>> FindAllByWhereAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public virtual async Task<TEntity> GetFirstWhereASync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
    {
        try
        {
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }

            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            await _context.SaveChangesAsync();

            return entityToUpdate;
        }
        catch (Exception ex)
        {
            // TODO: Repository excpetion
            throw;
        }
    }

    public virtual async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entitiesToUpdate)
    {
        var detachedEntities = new List<TEntity>();
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        foreach (var entityToUpdate in entitiesToUpdate)
        {
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                detachedEntities.Add(entityToUpdate);
            }

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        _dbSet.AttachRange(detachedEntities);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.AutoDetectChangesEnabled = true;

        return entitiesToUpdate;
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<IList<TEntity>> InsertRangeAsync(IList<TEntity> entities, bool saveChanges = true)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        if (saveChanges)
        {
            await _context.SaveChangesAsync();
        }

        return entities;
    }
}

