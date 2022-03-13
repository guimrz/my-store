namespace MyStore.Services.Catalog.Application.Responses.Products
{
    public class ProductResponse
    {
        public string? Name { get; set; }

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }

        public string? Sku { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public decimal Price { get; set; }

        public Guid BrandId { get; set; }
    }
}
