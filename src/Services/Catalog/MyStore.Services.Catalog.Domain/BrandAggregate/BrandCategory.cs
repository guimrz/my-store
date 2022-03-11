# nullable disable
using MyStore.Services.Catalog.Domain.CategoryAggregate;

namespace MyStore.Services.Catalog.Domain.BrandAggregate
{
    public class BrandCategory
    {
        public Guid Id { get; protected set; }

        public Guid BrandId { get; protected set; }

        public Guid CategoryId { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        #region Navigation properties
        public Category Category { get; protected set; }
        #endregion

        protected BrandCategory()
        {
            // Used by EF Core
        }

        public BrandCategory(Guid brandId, Guid categoryId)
        {
            BrandId = brandId;
            CategoryId = categoryId;
            CreationDate = DateTime.UtcNow;
        }
    }
}
