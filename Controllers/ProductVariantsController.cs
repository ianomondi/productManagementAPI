using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Resources;
using Supermarket.API.Domain.Services;
using Supermarket.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductVariantsController : ControllerBase
    {
        private readonly IProductVariantService _productVariantService;
        private readonly IMapper _mapper;

        public ProductVariantsController(IProductVariantService productVariantService, IMapper mapper)
        {
            _productVariantService = productVariantService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all categories.
        /// </summary>
        /// <returns>List os categories.</returns>
        [HttpGet("productVariants")]
        [ProducesResponseType(typeof(IEnumerable<ProductVariantResource>), 200)]
        public async Task<IEnumerable<ProductVariantResource>> ListAsync()
        {
            var categories = await _productVariantService.ListAsync();
            var resources = _mapper.Map<IEnumerable<ProductVariant>, IEnumerable<ProductVariantResource>>(categories);

            return resources;
        }

        /// <summary>
        /// Saves a new product variant.
        /// </summary>
        /// <param name="resource">Product variant data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPost("productVariant")]
        [ProducesResponseType(typeof(ProductVariantResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductVariantResource resource)
        {
            var productVariant = _mapper.Map<SaveProductVariantResource, ProductVariant>(resource);
            var result = await _productVariantService.SaveAsync(productVariant);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var productVariantResource = _mapper.Map<ProductVariant, ProductVariantResource>(result.Resource);
            return Ok(productVariantResource);
        }

        /// <summary>
        /// Updates an existing product variant according to an identifier.
        /// </summary>
        /// <param name="id">Product variant identifier.</param>
        /// <param name="resource">Updated product variant data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPut("productVariant/{id}")]
        [ProducesResponseType(typeof(ProductVariantResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductVariantResource resource)
        {
            var productVariant = _mapper.Map<SaveProductVariantResource, ProductVariant>(resource);
            var result = await _productVariantService.UpdateAsync(id, productVariant);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var productVariantResource = _mapper.Map<ProductVariant, ProductVariantResource>(result.Resource);
            return Ok(productVariantResource);
        }

        /// <summary>
        /// Deletes a given product variant according to an identifier.
        /// </summary>
        /// <param name="id">Product variant identifier.</param>
        /// <returns>Response for the request.</returns>

        [HttpDelete("productVariant/{id}")]
        [ProducesResponseType(typeof(ProductVariantResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productVariantService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var productVariantResource = _mapper.Map<ProductVariant, ProductVariantResource>(result.Resource);
            return Ok(productVariantResource);
        }
    }
}
