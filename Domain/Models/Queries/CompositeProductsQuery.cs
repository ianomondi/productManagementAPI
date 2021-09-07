using ProductManagement.API.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Models.Queries
{
    public class CompositeProductsQuery : Query
    {
        public int ProductId { get; set; }

        public CompositeProductsQuery(int productId):base(page:0,itemsPerPage:0)
        {
            ProductId = productId;
        }

        public CompositeProductsQuery(int productId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            ProductId = productId;
        }
    }
}
