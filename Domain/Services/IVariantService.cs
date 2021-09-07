using ProductManagement.API.Domain.Models;
using Supermarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services
{
    public interface IVariantService
    {
        Task<IEnumerable<Variant>> ListAsync();
        Task<VariantResponse> SaveAsync(Variant variant);
        Task<VariantResponse> UpdateAsync(int id, Variant variant);
        Task<VariantResponse> DeleteAsync(int id);
    }
}
