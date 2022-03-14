namespace MyStore.Services.Catalog.Application.Responses.Products
{
    public class ProductDetailResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Sku { get; set; }

        public decimal Price { get; set; }

        public string? ShortDescription  { get; set; }

        public string? FullDescription { get; set; }

        public ProductBrandResponse? Brand { get; set; }

        public IEnumerable<ProductCategoryResponse>? Categories { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
