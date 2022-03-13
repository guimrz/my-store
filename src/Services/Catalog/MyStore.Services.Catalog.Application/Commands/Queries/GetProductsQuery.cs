using FluentValidation;
using MediatR;
using MyStore.Services.Catalog.Application.Responses.Products;

namespace MyStore.Services.Catalog.Application.Commands.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductResponse>>
    {
        public int Count { get; set; }

        public int Offset { get; set; } = 20;

        public string? Search { get; set; }

        public Guid[]? Categories { get; set; }
    }

    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(p => p.Offset).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Count).GreaterThan(0);
        }
    }
}
