using ProductManagement.API.Domain.Models;
using Supermarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services
{
    public interface IVariantOptionService
    {
        Task<IEnumerable<VariantOption>> ListAsync();
        Task<VariantOptionResponse> SaveAsync(VariantOption variantOption);
        Task<VariantOptionResponse> UpdateAsync(int id, VariantOption variantOption);
        Task<VariantOptionResponse> DeleteAsync(int id);
    }
}
