using AutoMapper;
using MediatR;
using MyStore.Services.Catalog.Application.Commands;
using MyStore.Services.Catalog.Application.Responses;
using MyStore.Services.Catalog.Domain.CategoryAggregate;
using MyStore.Services.Catalog.Repository.Abstractions;

namespace MyStore.Services.Catalog.Application.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoriesRepository _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoriesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = new Category(request.Name, request.Description);

            category = await _repository.AddAsync(category, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
