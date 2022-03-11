using MyStore.Services.Catalog.Domain.CategoryAggregate;

#nullable disable
namespace MyStore.Services.Catalog.Domain.ProductAggregate
{
    public class ProductCategory
    {
        public Guid Id { get; protected set; }

        public Guid ProductId { get; protected set; }

        public Guid CategoryId { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        #region Navigation properties
        public Category Category { get; protected set; }
        #endregion

        protected ProductCategory()
        {
            // Used by EF Core
        }

        public ProductCategory(Guid productId, Guid categoryId)
        {
            CategoryId = categoryId;
            ProductId = productId;
            CreationDate = DateTime.UtcNow;
        }
    }
}
