using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class VariantOptionResource
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public string Value { get; set; }
    }
}
