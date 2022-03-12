using MediatR;
using MyStore.Services.Catalog.Application.Responses.Brands;

namespace MyStore.Services.Catalog.Application.Commands
{
    public record CreateBrandCommand : IRequest<BrandResponse>
    {
        public string? Name { get; set; }

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }

        public Guid[]? Categories { get; set; }
    }
}
