using AutoMapper;
using MyStore.Services.Catalog.Application.Responses;
using MyStore.Services.Catalog.Domain.CategoryAggregate;

namespace MyStore.Services.Catalog.Application.Mapping.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>();
        }
    }
}
