using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyStore.Services.Catalog.Application.Commands.Queries;
using MyStore.Services.Catalog.Application.Responses.Brands;
using MyStore.Services.Catalog.Domain.BrandAggregate;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Application.Handlers.Queries
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, IEnumerable<BrandResponse>>
    {
        private readonly IBrandsRepository _brandsRepository;
        private readonly IMapper _mapper;

        public GetBrandsQueryHandler(IBrandsRepository brandsRepository, IMapper mapper)
        {
            _brandsRepository = brandsRepository ?? throw new ArgumentNullException(nameof(brandsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<BrandResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Brand> categories = _brandsRepository.All
                .Include(p => p.Categories)
                    .ThenInclude(p => p.Category);

            if(request.Categories != null && request.Categories.Any())
            {
                categories = categories.Where(p => p.Categories.Any(p => request.Categories.Distinct().Contains(p.CategoryId)));
            }

            return Task.FromResult(_mapper.Map<IEnumerable<BrandResponse>>(categories.Skip(request.Offset).Take(request.Count)));
        }
    }
}
