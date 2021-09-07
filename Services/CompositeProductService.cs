using Microsoft.Extensions.Caching.Memory;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Infrastructure;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Services
{
    public class CompositeProductService : ICompositeProductService
    {
        private readonly ICompositeProductRepository _compositeProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CompositeProductService(IProductRepository productRepository, 
            ICategoryRepository categoryRepository, 
            ICompositeProductRepository compositeProductRepository, 
            IUnitOfWork unitOfWork, 
            IMemoryCache cache)
        {
            _compositeProductRepository = compositeProductRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<QueryResult<CompositeProduct>> ListAsync(CompositeProductsQuery query)
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.
            string cacheKey = GetCacheKeyForCompositeProductsQuery(query);

            var products = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _compositeProductRepository.ListAsync(query);
            });

            return products;
        }

        public async Task<CompositeProductResponse> SaveAsync(CompositeProduct product)
        {
            try
            {
                /*
                 Notice here we have to check if the category ID is valid before adding the product, to avoid errors.
                 You can create a method into the CategoryService class to return the category and inject the service here if you prefer, but 
                 it doesn't matter given the API scope.
                */
                var existingProduct = await _productRepository.FindByIdAsync(product.ProductId);
                if (existingProduct == null)
                    return new CompositeProductResponse("Invalid product.");

                await _compositeProductRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new CompositeProductResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompositeProductResponse($"An error occurred when saving the composite product: {ex.Message}");
            }
        }

        public async Task<CompositeProductResponse> UpdateAsync(int id, CompositeProduct product)
        {
            var existingCompositeProduct = await _compositeProductRepository.FindByIdAsync(id);

            if (existingCompositeProduct == null)
                return new CompositeProductResponse("Composite product not found.");

            var existingProduct = await _productRepository.FindByIdAsync(product.ProductId);
            if (existingProduct == null)
                return new CompositeProductResponse("Invalid product.");

            existingCompositeProduct.ProductId = product.RelatedId;

            try
            {
                _compositeProductRepository.Update(existingCompositeProduct);
                await _unitOfWork.CompleteAsync();

                return new CompositeProductResponse(existingCompositeProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompositeProductResponse($"An error occurred when updating the composite product: {ex.Message}");
            }
        }

        public async Task<CompositeProductResponse> DeleteAsync(int id)
        {
            var existingCompositeProduct = await _compositeProductRepository.FindByIdAsync(id);

            if (existingCompositeProduct == null)
                return new CompositeProductResponse("Composite product not found.");

            try
            {
                _compositeProductRepository.Remove(existingCompositeProduct);
                await _unitOfWork.CompleteAsync();

                return new CompositeProductResponse(existingCompositeProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompositeProductResponse($"An error occurred when deleting the composite product: {ex.Message}");
            }
        }




        private string GetCacheKeyForCompositeProductsQuery(CompositeProductsQuery query)
        {
            string key = CacheKeys.CompositeProductsList.ToString();

            if (query.ProductId > 0)
            {
                key = string.Concat(key, "_", query.ProductId);
            }

            key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
            return key;
        }

    }
}
