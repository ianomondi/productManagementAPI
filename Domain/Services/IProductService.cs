using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using ProductManagement.API.Domain.Services.Communication;

namespace ProductManagement.API.Domain.Services
{
    public interface IProductService
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}