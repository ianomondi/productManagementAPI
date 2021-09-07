using ProductManagement.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services
{
    public interface ISKUService
    {
        Task<SKUResponse> ListAsync(SKUsQuery query);
        Task<SKUResponse> SaveAsync(SKU sku);
        Task<SKUResponse> UpdateAsync(int id, SKU sku);
        Task<SKUResponse> DeleteAsync(int id);
    }
}
