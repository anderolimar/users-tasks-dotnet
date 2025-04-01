using System.Linq.Expressions;

namespace UsersTasks.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity?> FindByIdAsync(int id);
        Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(int id);
        Task<List<TEntity>> GetAllAsync(int offset, int limit);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, int offset, int limit);
        Task<int> CountAsync();
    }
}
