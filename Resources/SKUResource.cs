using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class SKUResource
    {
        public int Id { get; set; }
        public string UnitOfMeasure { get; set; }
        public string UnitPrice { get; set; }

        public int ProductId { get; set; }
        public string Quantity { get; set; }
    }
}
