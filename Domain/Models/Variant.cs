using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Domain.Models
{
    public class Variant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string FrontEndName { get; set; }

        public List<VariantOption> VariantOptions { get; set; }
        public List<ProductVariant> ProductVariants { get; set; }
    }
}
