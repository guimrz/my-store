using MyStore.Core.EntityFrameworkCore.Abstractions;
using MyStore.Services.Catalog.Domain.CategoryAggregate;

namespace MyStore.Services.Catalog.Repository.Abstractions
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        //
    }
}
