using ProductManagement.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Repositories
{
    public interface IVariantOptionRepository
    {
        Task<IEnumerable<VariantOption>> ListAsync();
        Task AddAsync(VariantOption variantOption);
        Task<VariantOption> FindByIdAsync(int id);
        void Update(VariantOption variantOption);
        void Remove(VariantOption variantOption);
    }
}
