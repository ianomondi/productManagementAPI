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
    public class VariantOptionService : IVariantOptionService
    {
        private readonly IVariantOptionRepository _variantOptionRepository;
        private readonly IVariantRepository _variantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public VariantOptionService(IVariantOptionRepository variantOptionRepository, IUnitOfWork unitOfWork, IMemoryCache cache, IVariantRepository variantRepository)
        {
            _variantOptionRepository = variantOptionRepository;
            _variantRepository = variantRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<VariantOption>> ListAsync()
        {
            // Here I try to get the categories list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the categories from the repository.
            var categories = await _cache.GetOrCreateAsync(CacheKeys.VariantOptionsList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _variantOptionRepository.ListAsync();
            });

            return categories;
        }

        public async Task<VariantOptionResponse> SaveAsync(VariantOption variantOption)
        {
            try
            {
                Variant variant = await _variantRepository.FindByIdAsync(variantOption.VariantId);

                if(variant == null)
                {
                    return new VariantOptionResponse($"Variant with id: {variantOption.VariantId} not found.");
                }

                await _variantOptionRepository.AddAsync(variantOption);
                await _unitOfWork.CompleteAsync();

                return new VariantOptionResponse(variantOption);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new VariantOptionResponse($"An error occurred when saving the Variant Option: {ex.Message}");
            }
        }

        public async Task<VariantOptionResponse> UpdateAsync(int id, VariantOption variantOption)
        {
            var existingVariantOption = await _variantOptionRepository.FindByIdAsync(id);

            if (existingVariantOption == null)
                return new VariantOptionResponse("Variant Option not found.");

            Variant variant = await _variantRepository.FindByIdAsync(variantOption.VariantId);

            if (variant == null)
            {
                return new VariantOptionResponse($"Variant with id: {variantOption.VariantId} not found.");
            }

            existingVariantOption.VariantId = variantOption.VariantId;
            existingVariantOption.Value = variantOption.Value;

            try
            {
                await _unitOfWork.CompleteAsync();

                return new VariantOptionResponse(existingVariantOption);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new VariantOptionResponse($"An error occurred when updating the Variant Option: {ex.Message}");
            }
        }

        public async Task<VariantOptionResponse> DeleteAsync(int id)
        {
            var existingVariantOption = await _variantOptionRepository.FindByIdAsync(id);

            if (existingVariantOption == null)
                return new VariantOptionResponse("Variant Option not found.");

            try
            {
                _variantOptionRepository.Remove(existingVariantOption);
                await _unitOfWork.CompleteAsync();

                return new VariantOptionResponse(existingVariantOption);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new VariantOptionResponse($"An error occurred when deleting the Variant Option: {ex.Message}");
            }
        }
    }
}
