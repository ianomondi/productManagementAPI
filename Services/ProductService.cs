using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Domain.Services;
using ProductManagement.API.Domain.Services.Communication;
using ProductManagement.API.Infrastructure;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Resources;

namespace ProductManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISKURepository _skuRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, 
            ICategoryRepository categoryRepository, 
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISKURepository sKURepository,
            IMemoryCache cache)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _skuRepository = sKURepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<QueryResult<Product>> ListAsync(ProductsQuery query)
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.
            string cacheKey = GetCacheKeyForProductsQuery(query);
            
            var products = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _productRepository.ListAsync(query);
            });

            List<Product> mappedProducts = null;

            if(products != null && products.Items != null)
            {
                mappedProducts = new List<Product>();
                products.Items.ForEach(async p =>
                {
                    try
                    {

                        if(p.SKUs != null)
                        {
                            SKU sku = p.SKUs[0];

                            SaveProductSKUResource saveSku = _mapper.Map<SKU, SaveProductSKUResource>(sku);

                            p.SaveProductSKUResource = saveSku;
                        }  

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                    /*try
                    {

                        if(p.ProductVariants != null)
                        {
                            List<ProductVariant> productVariants = p.ProductVariants;

                            List<ProductVariantResource> productVariantResources = _mapper.Map<List<ProductVariant>, List<ProductVariantResource>>(productVariants);

                            p.productVariantResources
                        }  

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }*/

                    mappedProducts.Add(p);
                });
            }

            products.Items = mappedProducts;

            return products;
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                /*
                 Notice here we have to check if the category ID is valid before adding the product, to avoid errors.
                 You can create a method into the CategoryService class to return the category and inject the service here if you prefer, but 
                 it doesn't matter given the API scope.
                */
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (existingCategory == null)
                    return new ProductResponse("Invalid category.");




                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();



                var productId = product.Id;
                var saveSku = product.SaveProductSKUResource;

                try
                {
                    if (saveSku != null)
                    {
                        var sku = _mapper.Map<SaveProductSKUResource, SKU>(saveSku);

                        sku.ProductId = productId;

                        _skuRepository.AddAsync(sku);

                        await _unitOfWork.CompleteAsync();

                        int skuId = sku.Id;


                    }
                }catch(Exception e)
                {
                    product.SaveProductSKUResource = null;
                }

                

                

                

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse("Product not found.");

            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
                return new ProductResponse("Invalid category.");

            existingProduct.Name = product.Name;
            //existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
            //existingProduct.QuantityInPackage = product.QuantityInPackage;
            existingProduct.CategoryId = product.CategoryId;

            try
            {
                _productRepository.Update(existingProduct);

                try
                {
                    List<SKU> skus = existingProduct.SKUs;

                    SKU sku = skus != null && skus.Count > 0 ? skus[0] : new SKU { ProductId = id };

                    if (product.SaveProductSKUResource != null)
                    {
                        sku.Quantity = product.SaveProductSKUResource.Quantity;
                        sku.UnitOfMeasure = product.SaveProductSKUResource.UnitOfMeasure;
                        sku.UnitPrice = product.SaveProductSKUResource.UnitPrice;

                        if (skus == null || skus.Count == 0) {
                            await _skuRepository.AddAsync(sku); 
                        }
                        else
                        {
                            _skuRepository.Update(sku);
                        }
                    }

                    SaveProductSKUResource saveSKU = _mapper.Map<SKU, SaveProductSKUResource>(sku);

                    existingProduct.SaveProductSKUResource = saveSKU;
                }
                catch(Exception e)
                {

                }

                


                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when updating the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse("Product not found.");

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }


       
       
        private string GetCacheKeyForProductsQuery(ProductsQuery query)
        {
            string key = CacheKeys.ProductsList.ToString();
            
            if (query.CategoryId.HasValue && query.CategoryId > 0)
            {
                key = string.Concat(key, "_", query.CategoryId.Value);
            }

            key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
            return key;
        }

        
    }
}