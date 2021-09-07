using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class CompositeProductResource
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int RelatedId { get; set; }
    }
}
