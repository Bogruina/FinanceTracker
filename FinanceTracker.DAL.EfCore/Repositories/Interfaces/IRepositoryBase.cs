using System.Linq.Expressions;

namespace FinanceTracker.DAL.EfCore.Repositories.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> Count();
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(ICollection<TEntity> entities);
    Task<List<TEntity>> FindAllByWhereAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetFirstWhereASync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<IList<TEntity>> InsertRangeAsync(IList<TEntity> entities, bool saveChanges = true);
    Task<TEntity> UpdateAsync(TEntity entityToUpdate);
    Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entitiesToUpdate);
}