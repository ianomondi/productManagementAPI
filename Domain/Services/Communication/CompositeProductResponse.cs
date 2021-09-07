using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services.Communication
{
    public class CompositeProductResponse : BaseResponse<CompositeProduct>
    {
        public CompositeProductResponse(CompositeProduct compositeProduct) : base(compositeProduct)
        { }

        public CompositeProductResponse(string message) : base(message)
        { }
    }
}
