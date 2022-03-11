using Microsoft.EntityFrameworkCore;
using MyStore.Services.Catalog.Repository.TypeConfigurations;

namespace MyStore.Services.Catalog.Repository
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductTypeConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
