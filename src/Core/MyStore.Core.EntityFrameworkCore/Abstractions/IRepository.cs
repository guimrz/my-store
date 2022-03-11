namespace MyStore.Core.EntityFrameworkCore.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public IQueryable<TEntity> All { get; }

        Task<TEntity> AddAsync(TEntity brand, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity brand, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(TEntity brand, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
