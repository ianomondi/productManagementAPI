using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class SaveProductSKUResource
    {
        [Required]
        public string UnitOfMeasure { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public string Quantity { get; set; }
    }
}
