using FluentValidation;
using MediatR;
using MyStore.Services.Catalog.Application.Responses.Brands;

namespace MyStore.Services.Catalog.Application.Commands.Queries
{
    public class GetBrandsQuery : IRequest<IEnumerable<BrandResponse>>
    {
        public int Count { get; set; } = 20;

        public int Offset { get; set; }

        public Guid[]? Categories { get; set; }
    }

    public class GetBrandsQueryValidator : AbstractValidator<GetBrandsQuery>
    {
        public GetBrandsQueryValidator()
        {
            RuleFor(p => p.Count).GreaterThan(0);
            RuleFor(p => p.Offset).GreaterThanOrEqualTo(0);
        }
    }
}
