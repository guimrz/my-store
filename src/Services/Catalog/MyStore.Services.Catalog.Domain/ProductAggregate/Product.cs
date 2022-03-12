#nullable disable
using MyStore.Services.Catalog.Domain.CategoryAggregate;
using MyStore.Services.Catalog.Domain.ProductAggregate;

namespace MyStore.Services.Catalog.Domain
{
    public class Product
    {
        private readonly List<ProductCategory> _categories;

        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public Guid BrandId { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string Sku { get; set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        public IEnumerable<ProductCategory> Categories { get; protected set; }

        public decimal Price { get; set; }

        #region Navigation properties
        public Brand Brand { get; set; }
        #endregion

        // Used by EF Core
        protected Product()
        {
            _categories = new List<ProductCategory>();
        }

        public Product(string name, string sku, decimal price, Brand brand, string shortDescription = null, string fullDescription = null) : this()
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException($"The value cannot be null, empty or whitespace.", nameof(name)) : name;
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "The value cannot be less than zero.");
            }

            Price = price;

            Sku = sku;
            ShortDescription = shortDescription;
            FullDescription = fullDescription;

            CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds a category to the product
        /// </summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ArgumentNullException">When the value of <paramref name="category"/> is null.</exception>
        public void Add(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _categories.Add(new ProductCategory(this.Id, category.Id));
        }
    }
}
