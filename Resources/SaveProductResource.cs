using ProductManagement.API.Domain.Models;
using Supermarket.API.Resources;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /*[Required]
        [Range(0, 100)]
        public short QuantityInPackage { get; set; }

        [Required]
        [Range(1, 5)]
        public int UnitOfMeasurement { get; set; }*/ // AutoMapper is going to cast it to the respective enum value
        
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public SaveProductSKUResource SaveProductSKUResource { get; set; }
    }
}