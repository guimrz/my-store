#nullable disable
namespace MyStore.Services.Catalog.Application.Responses.Brands
{
    public class BrandCategoryResponse
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
