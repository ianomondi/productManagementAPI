using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services.Communication
{
    public class SKUResponse : BaseResponse<SKU>
    {
        public SKUResponse(SKU sku) : base(sku)
        { }

        public SKUResponse(string message) : base(message)
        { }
    }
}
