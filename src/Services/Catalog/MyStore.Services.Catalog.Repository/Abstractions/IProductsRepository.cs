using MyStore.Core.EntityFrameworkCore.Abstractions;
using MyStore.Services.Catalog.Domain;

namespace MyStore.Services.Catalog.Repository.Abstractions
{
    public interface IProductsRepository : IRepository<Product>
    {
        //
    }
}
