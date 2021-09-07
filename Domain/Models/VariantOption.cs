using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Domain.Models
{
    public class VariantOption
    {
        public int Id { get; set; }
        [Required]
        public int VariantId { get; set; }
        [Required]
        public string Value { get; set; }

        public Variant Variant { get; set; }

        public List<ProductVariant> ProductVariants { get; set; }

    }
}
