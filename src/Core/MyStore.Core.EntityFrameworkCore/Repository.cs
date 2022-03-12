using Microsoft.EntityFrameworkCore;
using MyStore.Core.EntityFrameworkCore.Abstractions;

namespace MyStore.Core.EntityFrameworkCore
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext _dbContext;

        public IQueryable<TEntity> All => _dbContext.Set<TEntity>().AsNoTracking();

        protected Repository(DbContext dbContext)
        {
            _dbContext = dbContext?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await _dbContext.AddAsync(entity, cancellationToken);

            return entityEntry.Entity;
        }

        public Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = _dbContext.Remove(entity);

            return Task.FromResult(entityEntry.State == EntityState.Deleted);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = _dbContext.Update(entity);

            return Task.FromResult(entityEntry.Entity);
        }
    }
}
