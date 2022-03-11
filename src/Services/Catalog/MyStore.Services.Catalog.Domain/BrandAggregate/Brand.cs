#nullable disable
namespace MyStore.Services.Catalog.Domain
{
    public class Brand
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        protected Brand()
        {
            // Used by EF Core
        }

        public Brand(string name)
        {
            Name = name;
            CreationDate = DateTime.UtcNow;
        }
    }
}
