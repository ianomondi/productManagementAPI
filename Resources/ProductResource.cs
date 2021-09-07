using Supermarket.API.Resources;
using System.Collections.Generic;

namespace ProductManagement.API.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public int QuantityInPackage { get; set; }
        //public string UnitOfMeasurement { get; set; }
        public CategoryResource Category {get;set;}
        public List<ProductVariantResource> variants {get;set;}

        public SaveProductSKUResource sku { get; set; }
    }
}