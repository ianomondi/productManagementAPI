using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class SaveProductVariantResource
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int VariantOptionId { get; set; }
        [Required]
        public int VariantId { get; set; }

    }
}
