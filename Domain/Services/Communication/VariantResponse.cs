using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services.Communication
{
    public class VariantResponse : BaseResponse<Variant>
    {
        public VariantResponse(Variant variant) : base(variant) { }
        public VariantResponse(string message) : base(message) { }
    }
}
