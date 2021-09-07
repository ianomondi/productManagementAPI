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
    public class VariantRepository: BaseRepository, IVariantRepository
    {
        public VariantRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Variant>> ListAsync()
        {
            return await _context.Variants
                                 .AsNoTracking()
                                 .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(Variant variant)
        {
            await _context.Variants.AddAsync(variant);
        }

        public async Task<Variant> FindByIdAsync(int id)
        {
            return await _context.Variants.FindAsync(id);
        }

        public void Update(Variant variant)
        {
            _context.Variants.Update(variant);
        }

        public void Remove(Variant variant)
        {
            _context.Variants.Remove(variant);
        }
    }
}
