using AutoMapper;
using MediatR;
using MyStore.Services.Catalog.Application.Commands.Queries;
using MyStore.Services.Catalog.Application.Responses.Products;
using MyStore.Services.Catalog.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Services.Catalog.Application.Handlers.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _productsRepository.All;

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                products = products.Where(p => p.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }

            if (request.Categories != null && request.Categories.Any())
            {
                products = products.Where(product => product.Categories.Any(p => request.Categories.Distinct().Contains(p.CategoryId)));
            }

            products = products.Skip(request.Offset).Take(request.Count);

            return Task.FromResult(_mapper.Map<IEnumerable<ProductResponse>>(products));
        }
    }
}
