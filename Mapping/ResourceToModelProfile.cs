using AutoMapper;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using ProductManagement.API.Resources;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Resources;

namespace ProductManagement.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<SaveVariantResource, Variant>();
            CreateMap<SaveVariantOptionResource, VariantOption>();
            CreateMap<SaveProductVariantResource, ProductVariant>();
            CreateMap<SaveSKUResource, SKU>();
            CreateMap<SaveCompositeProductResource, CompositeProduct>();
            CreateMap<SaveProductResource, Product>();

            /*CreateMap<SaveProductResource, Product>()
                .ForMember(src => src.UnitOfMeasurement, opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement));*/

            CreateMap<ProductsQueryResource, ProductsQuery>();
            CreateMap<CompositeProductsQueryResource, CompositeProductsQuery>();
            CreateMap<SaveProductSKUResource, SKU>();
        }
    }
}