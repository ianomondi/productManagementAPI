using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Services.Communication;

namespace ProductManagement.API.Domain.Services
{
    public interface ICategoryService
    {
         Task<IEnumerable<Category>> ListAsync();
         Task<CategoryResponse> SaveAsync(Category category);
        Task<CategoryResponse> UpdateAsync(int id, Category category);
         Task<CategoryResponse> DeleteAsync(int id);
    }
}