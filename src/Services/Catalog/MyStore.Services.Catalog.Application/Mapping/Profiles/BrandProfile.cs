using AutoMapper;
using MyStore.Services.Catalog.Application.Responses.Brands;
using MyStore.Services.Catalog.Domain.BrandAggregate;

namespace MyStore.Services.Catalog.Application.Mapping.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandResponse>();
            CreateMap<BrandCategory, BrandCategoryResponse>()
                .ForMember(destination => destination.Name, map =>
                {
                    map.PreCondition(source => source.Category != null);
                    map.MapFrom(source => source.Category.Name);
                })
                .ForMember(destination => destination.Description, map =>
                {
                    map.PreCondition(source => source.Category != null);
                    map.MapFrom(source => source.Category.Description);
                })
                .ForMember(destination => destination.CreationDate, map => map.MapFrom(source => source.CreationDate))
                .ForMember(destination => destination.CategoryId, map => map.MapFrom(source => source.CategoryId));
        }
    }
}
