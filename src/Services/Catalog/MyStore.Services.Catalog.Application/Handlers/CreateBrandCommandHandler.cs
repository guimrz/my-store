using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyStore.Services.Catalog.Application.Commands;
using MyStore.Services.Catalog.Application.Responses.Brands;
using MyStore.Services.Catalog.Domain.BrandAggregate;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Application.Handlers
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandsRepository _brandsRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public CreateBrandCommandHandler(IBrandsRepository brandsRepository, ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _brandsRepository = brandsRepository ?? throw new ArgumentNullException(nameof(brandsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
        }

        public async Task<BrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = new Brand(request.Name, request.ShortDescription, request.FullDescription);

            if (request.Categories != null)
            {
                var categories = _categoriesRepository.All.Where(category => request.Categories.Distinct().Contains(category.Id));

                foreach (var category in categories)
                {
                    brand.Add(category);
                }
            }

            brand = await _brandsRepository.AddAsync(brand, cancellationToken);

            await _brandsRepository.SaveChangesAsync(cancellationToken);

            brand = await _brandsRepository.All
                .Include(p => p.Categories)
                    .ThenInclude(p => p.Category)
                .SingleAsync(p => p.Id == brand.Id);

            return _mapper.Map<BrandResponse>(brand);
        }
    }
}
