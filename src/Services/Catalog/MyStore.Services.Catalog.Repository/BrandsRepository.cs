using MyStore.Core.EntityFrameworkCore;
using MyStore.Services.Catalog.Domain.BrandAggregate;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Repository
{
    public class BrandsRepository : Repository<Brand>, IBrandsRepository
    {
        public BrandsRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            //
        }
    }
}
