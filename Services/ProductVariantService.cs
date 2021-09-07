using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Domain.Services.Communication;
using ProductManagement.API.Infrastructure;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IProductRepository _productRepository;
        private readonly IVariantRepository _variantRepository;
        private readonly IVariantOptionRepository _variantOptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public ProductVariantService(IProductVariantRepository productVariantRepository, 
            IUnitOfWork unitOfWork, 
            IMemoryCache cache, 
            IProductRepository productRepository, 
            IVariantOptionRepository variantOptionRepository,
            IMapper mapper,
            IVariantRepository variantRepository)
        {
            _productVariantRepository = productVariantRepository;
            _productRepository = productRepository;
            _variantRepository = variantRepository;
            _variantOptionRepository = variantOptionRepository;
            _productVariantRepository = productVariantRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVariant>> ListAsync()
        {
            // Here I try to get the categories list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the categories from the repository.
            var categories = await _cache.GetOrCreateAsync(CacheKeys.ProductsList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _productVariantRepository.ListAsync();
            });

            return categories;
        }

        public async Task<ProductVariantResponse> SaveAsync(ProductVariant productVariant)
        {
            try
            {

                Variant variant = await _variantRepository.FindByIdAsync(productVariant.VariantId);
                Product product = await _productRepository.FindByIdAsync(productVariant.ProductId);
                VariantOption variantOption = await _variantOptionRepository.FindByIdAsync(productVariant.VariantOptionId);

                if(product == null)
                {
                    return new ProductVariantResponse($"Product with id: {productVariant.ProductId} not found");
                }
                if(variant == null)
                {
                    return new ProductVariantResponse($"Variant with id: {productVariant.VariantId} not found");
                }
                if(variantOption == null)
                {
                    return new ProductVariantResponse($"Variant option with id: {productVariant.VariantOptionId} not found");
                }

                if(variant.Id != variantOption.VariantId)
                {
                    return new ProductVariantResponse($"Variant option {variantOption.Value} does not belong to variant: {variant.Name}");
                }


                await _productVariantRepository.AddAsync(productVariant);
                await _unitOfWork.CompleteAsync();

                return new ProductVariantResponse(productVariant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductVariantResponse($"An error occurred when saving the product variant: {ex.Message}");
            }
        }

        public async Task<ProductVariantResponse> UpdateAsync(int id, ProductVariant productVariant)
        {
            var existingProductVariant = await _productVariantRepository.FindByIdAsync(id);

            if (existingProductVariant == null)
                return new ProductVariantResponse("ProductVariant not found.");

            Variant variant = await _variantRepository.FindByIdAsync(productVariant.VariantId);
            Product product = await _productRepository.FindByIdAsync(productVariant.ProductId);
            VariantOption variantOption = await _variantOptionRepository.FindByIdAsync(productVariant.VariantOptionId);

            if (product == null)
            {
                return new ProductVariantResponse($"Product with id: {productVariant.ProductId} not found");
            }
            if (variant == null)
            {
                return new ProductVariantResponse($"Variant with id: {productVariant.VariantId} not found");
            }
            if (variantOption == null)
            {
                return new ProductVariantResponse($"Variant option with id: {productVariant.VariantOptionId} not found");
            }

            if (variant.Id != variantOption.VariantId)
            {
                return new ProductVariantResponse($"Variant option {variantOption.Value} does not belong to variant: {variant.Name}");
            }

            existingProductVariant.ProductId = productVariant.ProductId;
            existingProductVariant.VariantId = productVariant.VariantId;
            existingProductVariant.VariantOptionId = productVariant.VariantOptionId;

            try
            {
                await _unitOfWork.CompleteAsync();

                return new ProductVariantResponse(existingProductVariant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductVariantResponse($"An error occurred when updating the product variant: {ex.Message}");
            }
        }

        public async Task<ProductVariantResponse> DeleteAsync(int id)
        {
            var existingProductVariant = await _productVariantRepository.FindByIdAsync(id);

            if (existingProductVariant == null)
                return new ProductVariantResponse("Product variant not found.");

            try
            {
                _productVariantRepository.Remove(existingProductVariant);
                await _unitOfWork.CompleteAsync();

                return new ProductVariantResponse(existingProductVariant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductVariantResponse($"An error occurred when deleting the product variant: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ProductVariantResponse>> SaveRangeAsync(List<ProductVariant> productVariants)
        {
            await _productVariantRepository.AddRangeAsync(productVariants);

            await _unitOfWork.CompleteAsync();

            IEnumerable<ProductVariantResponse> productVariantResponses = _mapper.Map<IEnumerable<ProductVariant>, IEnumerable<ProductVariantResponse>>(productVariants);

            return productVariantResponses;
        }
    }
}
