using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services.Communication
{
    public class VariantOptionResponse : BaseResponse<VariantOption>
    {
        public VariantOptionResponse(VariantOption variantOption) : base(variantOption) { }
        public VariantOptionResponse(string message) : base(message) { }
    }
}
