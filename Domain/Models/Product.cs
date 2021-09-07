using Supermarket.API.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.API.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public short QuantityInPackage { get; set; }
        //public EUnitOfMeasurement UnitOfMeasurement { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [AutoMapper.Configuration.Conventions.MapTo("variants")]
        public List<ProductVariant> ProductVariants { get; set; }
        public List<CompositeProduct> CompositeProducts { get; set; }
        public List<SKU> SKUs { get; set; }

        [NotMapped]
        [AutoMapper.Configuration.Conventions.MapTo("sku")]
        public SaveProductSKUResource SaveProductSKUResource { get; set; }
        

    }
}