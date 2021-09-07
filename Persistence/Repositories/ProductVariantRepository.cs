using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Persistence.Contexts;
using ProductManagement.API.Persistence.Repositories;
using Supermarket.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Persistence.Repositories
{
    public class ProductVariantRepository : BaseRepository, IProductVariantRepository
    {

        public ProductVariantRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductVariant>> ListAsync()
        {
            return await _context.ProductVariants
                                 .AsNoTracking()
                                 .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(ProductVariant productVariant)
        {
            await _context.ProductVariants.AddAsync(productVariant);
        }

        public async Task<ProductVariant> FindByIdAsync(int id)
        {
            return await _context.ProductVariants.FindAsync(id);
        }

        public void Update(ProductVariant productVariant)
        {
            _context.ProductVariants.Update(productVariant);
        }

        public void Remove(ProductVariant productVariant)
        {
            _context.ProductVariants.Remove(productVariant);
        }

        public async Task AddRangeAsync(List<ProductVariant> productVariants)
        {
            try
            {
                if (productVariants != null)
                {

                    var filtered = new HashSet<ProductVariant>(productVariants);

                    await _context.ProductVariants.AddRangeAsync(filtered);
                }

            }
            catch (Exception e)
            {

            }
            
        }
    }
}
