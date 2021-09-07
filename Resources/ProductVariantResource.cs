using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class ProductVariantResource
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VariantOptionId { get; set; }
        public int VariantId { get; set; }
    }
}
