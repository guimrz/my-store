using AutoMapper;
using MyStore.Services.Catalog.Application.Responses.Products;
using MyStore.Services.Catalog.Domain;

namespace MyStore.Services.Catalog.Application.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}
