using Microsoft.Extensions.Caching.Memory;
using ProductManagement.API.Domain.Models;
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
    public class SKUService : ISKUService
    {
        private readonly ISKURepository _skuRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public SKUService(ISKURepository skuRepository, 
            IProductRepository productRepository,
            IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _skuRepository = skuRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<SKUResponse> ListAsync(SKUsQuery query)
        {
            // Here I try to get the categories list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the categories from the repository.
            
            
            var sku = await _skuRepository.ListAsync(query);

            if(sku == null)
            {
                return new SKUResponse($"SKU for product id: {query.ProductId} does not exist.");
            }

            return new SKUResponse(sku);

            
        }

        public async Task<SKUResponse> SaveAsync(SKU sku)
        {
            try
            {
                var existingSKU = await _skuRepository.FindByProductIdAsync(sku.ProductId);

                if (existingSKU != null)
                    return new SKUResponse($"SKU for the product id {sku.ProductId} already exists.");

                Product product = await _productRepository.FindByIdAsync(sku.ProductId);

                if(product == null)
                {
                    return new SKUResponse($"Product with id: {sku.ProductId} does not exist");
                }

                await _skuRepository.AddAsync(sku);
                await _unitOfWork.CompleteAsync();

                return new SKUResponse(sku);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SKUResponse($"An error occurred when saving the sku: {ex.Message}");
            }
        }

        public async Task<SKUResponse> UpdateAsync(int id, SKU sku)
        {
            var existingSKU = await _skuRepository.FindByIdAsync(id);

            if (existingSKU == null)
                return new SKUResponse("SKU not found.");

            Product product = await _productRepository.FindByIdAsync(sku.ProductId);

            if (product == null)
            {
                return new SKUResponse($"Product with id: {sku.ProductId} does not exist");
            }

            existingSKU.ProductId = sku.ProductId;
            existingSKU.Quantity = sku.Quantity;
            existingSKU.UnitOfMeasure = sku.UnitOfMeasure;

            try
            {
                await _unitOfWork.CompleteAsync();

                return new SKUResponse(existingSKU);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SKUResponse($"An error occurred when updating the sku: {ex.Message}");
            }
        }

        public async Task<SKUResponse> DeleteAsync(int id)
        {
            var existingSKU = await _skuRepository.FindByIdAsync(id);

            if (existingSKU == null)
                return new SKUResponse("SKU not found.");

            try
            {
                _skuRepository.Remove(existingSKU);
                await _unitOfWork.CompleteAsync();

                return new SKUResponse(existingSKU);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SKUResponse($"An error occurred when deleting the sku: {ex.Message}");
            }
        }
    }
}
