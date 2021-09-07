using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Persistence.Contexts;
using ProductManagement.API.Persistence.Repositories;
using Supermarket.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Persistence.Repositories
{
    public class VariantOptionRepository : BaseRepository, IVariantOptionRepository
    {
        public VariantOptionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<VariantOption>> ListAsync()
        {
            return await _context.VariantOptions
                                 .AsNoTracking()
                                 .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(VariantOption variantOption)
        {
            await _context.VariantOptions.AddAsync(variantOption);
        }

        public async Task<VariantOption> FindByIdAsync(int id)
        {
            return await _context.VariantOptions.FindAsync(id);
        }

        public void Update(VariantOption variantOption)
        {
            _context.VariantOptions.Update(variantOption);
        }

        public void Remove(VariantOption variantOption)
        {
            _context.VariantOptions.Remove(variantOption);
        }
    }
}
