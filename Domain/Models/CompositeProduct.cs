using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Domain.Models
{
    public class CompositeProduct
    {

        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int RelatedId { get; set; }

        //public Product Product { get; set; }
        //public Product Related { get; set; }

    }
}
