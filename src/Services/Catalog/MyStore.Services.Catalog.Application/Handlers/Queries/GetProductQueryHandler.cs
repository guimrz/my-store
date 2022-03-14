using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyStore.Core.Mvc.Exceptions;
using MyStore.Services.Catalog.Application.Commands.Queries;
using MyStore.Services.Catalog.Application.Responses.Products;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Application.Handlers.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDetailResponse>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDetailResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.All
                .Include(p => p.Categories)
                    .ThenInclude(p => p.Category)
                .Include(p => p.Brand)
                .SingleOrDefaultAsync(p => p.Id == request.ProductId);

            if (product == null)
            {
                throw new NotFoundException($"The product with id '{request.ProductId}' could not be found.");
            }

            return _mapper.Map<ProductDetailResponse>(product);
        }
    }
}
