using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
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
    public class CompositeProductRepository : BaseRepository, ICompositeProductRepository
    {
		public CompositeProductRepository(AppDbContext context) : base(context) { }

		public async Task<QueryResult<CompositeProduct>> ListAsync(CompositeProductsQuery query)
		{
			IQueryable<CompositeProduct> queryable = _context.CompositeProducts
													.AsNoTracking();

			// AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
			// tracking makes the code a little faster
			queryable = queryable.Where(p => p.ProductId == query.ProductId);

			// Here I count all items present in the database for the given query, to return as part of the pagination data.
			int totalItems = await queryable.CountAsync();

			// Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
			// and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
			List<CompositeProduct> products = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
													.Take(query.ItemsPerPage)
													.ToListAsync();

			// Finally I return a query result, containing all items and the amount of items in the database (necessary for client-side calculations ).
			return new QueryResult<CompositeProduct>
			{
				Items = products,
				TotalItems = totalItems,
			};
		}

		public async Task<CompositeProduct> FindByIdAsync(int id)
		{
			return await _context.CompositeProducts
								 .FirstOrDefaultAsync(p => p.Id == id); // Since Include changes the method's return type, we can't use FindAsync
		}

		public async Task AddAsync(CompositeProduct product)
		{
			await _context.CompositeProducts.AddAsync(product);
		}

		public void Update(CompositeProduct product)
		{
			_context.CompositeProducts.Update(product);
		}

		public void Remove(CompositeProduct product)
		{
			_context.CompositeProducts.Remove(product);
		}
	}
}
