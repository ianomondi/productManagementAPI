using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Domain.Models
{
    public class ProductVariant
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int VariantId { get; set; }
        [Required]
        public int VariantOptionId { get; set; }

        public Product Product { get; set; }
        public VariantOption VariantOption { get; set; }
        public Variant Variant { get; set; }

        
    }
}
