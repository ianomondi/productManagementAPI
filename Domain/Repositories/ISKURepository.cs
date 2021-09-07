using ProductManagement.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Repositories
{
    public interface ISKURepository
    {
        Task<SKU> ListAsync(SKUsQuery query);
        Task AddAsync(SKU sku);
        Task<SKU> FindByIdAsync(int id);
        Task<SKU> FindByProductIdAsync(int product_id);
        void Update(SKU sku);
        void Remove(SKU sku);
    }
}
