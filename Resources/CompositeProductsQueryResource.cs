using ProductManagement.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class CompositeProductsQueryResource: QueryResource
    {
        public int ProductId { get; set; }
    }
}
