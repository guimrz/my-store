using AutoMapper;
using MyStore.Services.Catalog.Application.Responses.Products;
using MyStore.Services.Catalog.Domain;
using MyStore.Services.Catalog.Domain.BrandAggregate;
using MyStore.Services.Catalog.Domain.ProductAggregate;

namespace MyStore.Services.Catalog.Application.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<Product, ProductDetailResponse>();
            CreateMap<ProductCategory, ProductCategoryResponse>()
                .ForMember(target => target.Name, map => 
                {
                    map.PreCondition(source => source.Category != null);
                    map.MapFrom(source => source.Category.Name);
                });
            CreateMap<Brand, ProductBrandResponse>();
        }
    }
}
