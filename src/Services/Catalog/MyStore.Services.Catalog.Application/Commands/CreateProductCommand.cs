using FluentValidation;
using MediatR;
using MyStore.Services.Catalog.Application.Responses.Products;

namespace MyStore.Services.Catalog.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDetailResponse>
    {
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Sku { get; set; }

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }

        public Guid BrandId { get; set; }

        public Guid[]? Categories { get; set; }
    }

    public class CreateProductCommandVaidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandVaidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(128);
            RuleFor(p => p.ShortDescription).MaximumLength(512);
            RuleFor(p => p.Sku).MaximumLength(128);
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.BrandId).NotEmpty();
        }
    }
}
