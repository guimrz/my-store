using FluentValidation;
using MediatR;
using MyStore.Services.Catalog.Application.Responses.Categories;

namespace MyStore.Services.Catalog.Application.Commands
{
    public record CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
    }

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
            RuleFor(p => p.Description).MaximumLength(512);
        }
    }
}
