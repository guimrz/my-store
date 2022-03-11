#nullable disable
namespace MyStore.Services.Catalog.Domain.CategoryAggregate
{
    public class Category
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        public Category()
        {
            // Used by EF Core
        }

        public Category(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("The value cannot be null, empty or whitespaces.", nameof(name)) : name;
            CreationDate = DateTime.UtcNow;
        }
    }
}
