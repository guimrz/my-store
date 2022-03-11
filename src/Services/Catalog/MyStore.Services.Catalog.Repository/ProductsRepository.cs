using MyStore.Core.EntityFrameworkCore;
using MyStore.Services.Catalog.Domain;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Repository
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            //
        }
    }
}
