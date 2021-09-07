using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services.Communication
{
    public class ProductVariantResponse: BaseResponse<ProductVariant>
    {
        public ProductVariantResponse(ProductVariant productVariant) : base(productVariant)
        { }

        public ProductVariantResponse(string message) : base(message)
        { }
    }
}
