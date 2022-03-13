using FluentValidation;
using MediatR;
using MyStore.Services.Catalog.Application.Responses.Categories;

namespace MyStore.Services.Catalog.Application.Commands.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>
    {
        public int Count { get; set; } = 40;

        public int Offset { get; set; }
    }

    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        { 
            RuleFor(p => p.Count).GreaterThan(0);
            RuleFor(p => p.Offset).GreaterThanOrEqualTo(0);
        }
    }
}
