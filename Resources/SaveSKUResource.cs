using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class SaveSKUResource
    {

        [Required]
        public string UnitOfMeasure { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]
        public string Quantity { get; set; }

    }
}
