using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services
{
    public interface ICompositeProductService
    {
        Task<QueryResult<CompositeProduct>> ListAsync(CompositeProductsQuery query);
        Task<CompositeProductResponse> SaveAsync(CompositeProduct compositeProduct);
        Task<CompositeProductResponse> UpdateAsync(int id, CompositeProduct compositeProduct);
        Task<CompositeProductResponse> DeleteAsync(int id);
    }
}
