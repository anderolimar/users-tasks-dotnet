
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UsersTasks.Data;
using UsersTasks.Interfaces;

namespace UsersTasks.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        public Repository(AppDbContext dBContext) { _dbContext = dBContext;  }

        public async Task<TEntity> InsertAsync(TEntity entity) { 
            var newEntity = _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task UpdateAsync(TEntity entity) {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> FindByIdAsync(int id) {
            TEntity? model = await _dbContext.Set<TEntity>().FindAsync(id);
            return model;
        }

        public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate) {

            TEntity? model = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return model;
        }

        public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, int offset, int limit)
        {
            return await _dbContext.Set<TEntity>().Where(predicate)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id) {
            TEntity? model = await FindByIdAsync(id);
            if (model != null) {
                _dbContext.Set<TEntity>().Remove(model);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TEntity>> GetAllAsync(int offset, int limit)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking()
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }
    }
}
