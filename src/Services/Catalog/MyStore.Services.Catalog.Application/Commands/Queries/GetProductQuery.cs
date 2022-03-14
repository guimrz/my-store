using MediatR;
using MyStore.Services.Catalog.Application.Responses.Products;

namespace MyStore.Services.Catalog.Application.Commands.Queries
{
    public class GetProductQuery : IRequest<ProductDetailResponse>
    {
        public Guid ProductId { get; set; }
    }
}
