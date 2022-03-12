#nullable disable
namespace MyStore.Services.Catalog.Application.Responses.Brands
{
    public class BrandResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public IEnumerable<BrandCategoryResponse> Categories { get; set; }
    }
}
