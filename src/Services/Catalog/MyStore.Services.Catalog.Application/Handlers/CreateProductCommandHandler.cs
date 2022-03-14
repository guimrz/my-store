using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyStore.Core.Mvc.Exceptions;
using MyStore.Services.Catalog.Application.Commands;
using MyStore.Services.Catalog.Application.Responses.Products;
using MyStore.Services.Catalog.Domain;
using MyStore.Services.Catalog.Domain.BrandAggregate;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDetailResponse>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IBrandsRepository _brandsRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository, IBrandsRepository brandsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
            _brandsRepository = brandsRepository ?? throw new ArgumentNullException(nameof(brandsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDetailResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandsRepository.All.SingleOrDefaultAsync(brand => brand.Id == request.BrandId);

            if (brand == null)
            {
                throw new NotFoundException($"The brand with id '{request.BrandId}' could not be found.");
            }

            Product product = new Product(request.Name, request.Sku, request.Price, brand, request.ShortDescription, request.FullDescription);

            if (request.Categories != null)
            {
                var categories = _categoriesRepository.All.Where(category => request.Categories.Distinct().Contains(category.Id));

                foreach (var category in categories)
                {
                    product.Add(category);
                }
            }

            product = await _productsRepository.AddAsync(product);
            await _productsRepository.SaveChangesAsync(cancellationToken);

            product = await _productsRepository.All
                .Include(p => p.Categories)
                    .ThenInclude(p => p.Category)
                .Include(p => p.Brand)
                .SingleAsync(p => p.Id == product.Id);

            return _mapper.Map<ProductDetailResponse>(product);
        }
    }
}
