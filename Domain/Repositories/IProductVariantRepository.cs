using ProductManagement.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Repositories
{
    public interface IProductVariantRepository
    {
        Task<IEnumerable<ProductVariant>> ListAsync();
        Task AddAsync(ProductVariant productVariant);
        Task AddRangeAsync(List<ProductVariant> productVariants);
        Task<ProductVariant> FindByIdAsync(int id);
        void Update(ProductVariant productVariant);
        void Remove(ProductVariant productVariant);
    }
}
