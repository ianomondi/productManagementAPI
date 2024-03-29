using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Services.Communication;

namespace ProductManagement.API.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int id);
        void Update(Category category);
        void Remove(Category category);
    }
}