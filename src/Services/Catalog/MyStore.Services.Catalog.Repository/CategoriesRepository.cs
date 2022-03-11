using MyStore.Core.EntityFrameworkCore;
using MyStore.Services.Catalog.Domain.CategoryAggregate;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Repository
{
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            //
        }
    }
}
