using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Persistence.Contexts;
using ProductManagement.API.Persistence.Repositories;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Persistence.Repositories
{
    public class SKURepository : BaseRepository, ISKURepository
    {
        public SKURepository(AppDbContext context) : base(context) { }

        public async Task<SKU> ListAsync(SKUsQuery query)
        {
            /*IQueryable<SKU> queryable = await _context.SKUs
                                 .AsNoTracking()
                                 .ToListAsync();*/

            return await _context.SKUs.FirstOrDefaultAsync(p => p.ProductId == query.ProductId) ?? null;



            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(SKU sku)
        {
            await _context.SKUs.AddAsync(sku);
        }

        public async Task<SKU> FindByIdAsync(int id)
        {
            return await _context.SKUs.FindAsync(id);
        }

        public void Update(SKU sku)
        {
            _context.SKUs.Update(sku);
        }

        public void Remove(SKU sku)
        {
            _context.SKUs.Remove(sku);
        }

        public async Task<SKU> FindByProductIdAsync(int product_id)
        {
            return await _context.SKUs.FirstOrDefaultAsync(s => s.ProductId == product_id);
        }
    }
}
