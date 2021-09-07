using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Supermarket.API.Domain.Models.Queries
{
    public class SKUsQuery : Query
    {
        public int ProductId { get; set; }

        public SKUsQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
