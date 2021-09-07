using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Domain.Models
{
    public class SKU
    {
        public int Id { get; set; }
        [Required]
        public string UnitOfMeasure { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]
        public string Quantity { get; set; }

        public Product Product { get; set; }

    }
}
