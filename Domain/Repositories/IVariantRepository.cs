using ProductManagement.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Repositories
{
    public interface IVariantRepository
    {
        Task<IEnumerable<Variant>> ListAsync();
        Task AddAsync(Variant variantss);
        Task<Variant> FindByIdAsync(int id);
        void Update(Variant variant);
        void Remove(Variant variant);
    }
}
