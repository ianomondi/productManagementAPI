using ProductManagement.API.Domain.Models;
using Supermarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services
{
    public interface IProductVariantService
    {
        Task<IEnumerable<ProductVariant>> ListAsync();
        Task<ProductVariantResponse> SaveAsync(ProductVariant productVariant);
        Task<IEnumerable<ProductVariantResponse>> SaveRangeAsync(List<ProductVariant> productVariants);
        Task<ProductVariantResponse> UpdateAsync(int id, ProductVariant productVariant);
        Task<ProductVariantResponse> DeleteAsync(int id);
    }
}
