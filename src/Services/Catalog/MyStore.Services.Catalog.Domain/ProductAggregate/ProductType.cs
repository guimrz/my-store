#nullable disable
namespace MyStore.Services.Catalog.Domain
{
    public class ProductType
    {
        public int Id { get; protected set; } 

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate  { get; set; }

        public DateTime? UpdateDate { get; set; }

        protected ProductType()
        {
            // Used by EF Core
        }

        public ProductType(string name, string description)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("The value cannot be null, empty or whitespaces.") : name;
            Description = description;
            CreationDate = DateTime.UtcNow;
        }
    }
}
