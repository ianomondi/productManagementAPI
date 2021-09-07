using Microsoft.Extensions.Caching.Memory;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Repositories;
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
    public class VariantService: IVariantService
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IVariantOptionRepository _variantOptionRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public VariantService(IVariantRepository variantRepository, 
            IUnitOfWork unitOfWork,
            IVariantOptionRepository variantOptionRepository,
            IProductVariantRepository productVariantRepository,
            IMemoryCache cache)
        {
            _variantRepository = variantRepository;
            _variantOptionRepository = variantOptionRepository;
            _productVariantRepository = productVariantRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Variant>> ListAsync()
        {
            // Here I try to get the variants list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the variants from the repository.
            var variants = await _cache.GetOrCreateAsync(CacheKeys.VariantsList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _variantRepository.ListAsync();
            });


            return variants;
        }

        public async Task<VariantResponse> SaveAsync(Variant variant)
        {
            try
            {
                await _variantRepository.AddAsync(variant);
                await _unitOfWork.CompleteAsync();

                return new VariantResponse(variant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new VariantResponse($"An error occurred when saving the variant: {ex.Message}");
            }
        }

        public async Task<VariantResponse> UpdateAsync(int id, Variant variant)
        {
            var existingVariant = await _variantRepository.FindByIdAsync(id);

            if (existingVariant == null)
                return new VariantResponse("Variant not found.");

            existingVariant.Name = variant.Name;
            existingVariant.DisplayName = variant.DisplayName;
            existingVariant.FrontEndName = variant.FrontEndName;

            try
            {
                await _unitOfWork.CompleteAsync();

                return new VariantResponse(existingVariant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new VariantResponse($"An error occurred when updating the variant: {ex.Message}");
            }
        }

        public async Task<VariantResponse> DeleteAsync(int id)
        {
            var existingVariant = await _variantRepository.FindByIdAsync(id);

            if (existingVariant == null)
                return new VariantResponse("Variant not found.");

            try
            {

                IEnumerable<VariantOption> variantOptions = await _variantOptionRepository.ListAsync();
                variantOptions = variantOptions.Where(vo => vo.VariantId == id);

                IEnumerable<ProductVariant> productVariants = await _productVariantRepository.ListAsync();

                List<ProductVariant> filteredProductVariants = new List<ProductVariant>();

                if(variantOptions != null && variantOptions.Count() > 0)
                {
                    variantOptions.ToList().ForEach(vo =>
                    {
                        List<ProductVariant> pvs = productVariants.Where(pv => pv.VariantOptionId == vo.Id).ToList();

                        filteredProductVariants.AddRange(pvs);
                    });
                }

                if(filteredProductVariants.Count() > 0)
                {
                    filteredProductVariants.ToList().ForEach(pv => _productVariantRepository.Remove(pv));
                }

                if (variantOptions != null && variantOptions.Count() > 0)
                {
                    variantOptions.ToList().ForEach(vo => _variantOptionRepository.Remove(vo));
                }

                    _variantRepository.Remove(existingVariant);
                await _unitOfWork.CompleteAsync();

                return new VariantResponse(existingVariant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new VariantResponse($"An error occurred when deleting the variant: {ex.Message}");
            }
        }
    }
}
