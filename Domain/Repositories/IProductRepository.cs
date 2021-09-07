using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using ProductManagement.API.Domain.Services.Communication;

namespace ProductManagement.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task AddAsync(Product product);
        Task<Product> FindByIdAsync(int id);
        void Update(Product product);
        void Remove(Product product);
    }
}