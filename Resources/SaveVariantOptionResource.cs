using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Resources
{
    public class SaveVariantOptionResource
    {
        [Required]
        public int VariantId { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
