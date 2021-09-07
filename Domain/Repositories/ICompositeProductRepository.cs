using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using Supermarket.API.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Repositories
{
    public interface ICompositeProductRepository
    {
        Task<QueryResult<CompositeProduct>> ListAsync(CompositeProductsQuery query);
        Task AddAsync(CompositeProduct product);
        Task<CompositeProduct> FindByIdAsync(int id);
        void Update(CompositeProduct product);
        void Remove(CompositeProduct product);
    }
}
