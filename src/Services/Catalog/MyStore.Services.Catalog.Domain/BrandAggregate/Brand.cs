#nullable disable
using MyStore.Services.Catalog.Domain.BrandAggregate;
using MyStore.Services.Catalog.Domain.CategoryAggregate;

namespace MyStore.Services.Catalog.Domain
{
    public class Brand
    {
        private readonly List<BrandCategory> _categories;

        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        public IEnumerable<BrandCategory> Categories { get => _categories; }

        // Used by EF Core
        protected Brand()
        {
            _categories = new List<BrandCategory>();
        }

        public Brand(string name) : this()
        {
            Name = name;
            CreationDate = DateTime.UtcNow;
        }

        public void AddCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _categories.Add(new BrandCategory(this.Id, category.Id));
        }
    }
}
