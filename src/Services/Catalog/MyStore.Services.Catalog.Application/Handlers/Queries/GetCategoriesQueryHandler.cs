using AutoMapper;
using MediatR;
using MyStore.Services.Catalog.Application.Commands.Queries;
using MyStore.Services.Catalog.Application.Responses.Categories;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Application.Handlers.Queries
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _categoriesRepository.All.Skip(request.Offset).Take(request.Count);

            return Task.FromResult(_mapper.Map<IEnumerable<CategoryResponse>>(categories));
        }
    }
}
