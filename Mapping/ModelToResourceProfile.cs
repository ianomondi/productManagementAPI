using AutoMapper;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using ProductManagement.API.Extensions;
using ProductManagement.API.Resources;
using Supermarket.API.Resources;

namespace ProductManagement.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Variant, VariantResource>();
            CreateMap<VariantOption, VariantOptionResource>();
            CreateMap<ProductVariant, ProductVariantResource>();
            CreateMap<SKU, SKUResource>();
            CreateMap<SKU, SaveProductSKUResource>();
            CreateMap<CompositeProduct, CompositeProductResource>();
            CreateMap<Product, ProductResource>();

            /*CreateMap<Product, ProductResource>()
                .ForMember(src => src.UnitOfMeasurement,
                           opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));*/

            CreateMap<QueryResult<Product>, QueryResultResource<ProductResource>>();
            CreateMap<QueryResult<CompositeProduct>, QueryResultResource<CompositeProductResource>>();
        }
    }
}