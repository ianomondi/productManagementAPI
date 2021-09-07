using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Resources;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SkuController : ControllerBase
    {
        private readonly ISKUService _skuService;
        private readonly IMapper _mapper;

        public SkuController(ISKUService skuService, IMapper mapper)
        {
            _skuService = skuService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all skus.
        /// </summary>
        /// <returns>List os skus.</returns>
        [HttpGet("sku/{id}")]
        //[ProducesResponseType(typeof(IEnumerable<SKUResource>), 200)]
        [ProducesResponseType(typeof(SKUResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> ListAsync(int id)
        {
            SKUsQuery sKUsQuery = new SKUsQuery(id);

            SKUResponse skuResponse = await _skuService.ListAsync(sKUsQuery);

            if (skuResponse.Success)
            {
                var resource = skuResponse.Resource;

                var mapped = _mapper.Map<SKU, SKUResource>(resource);

                return Ok(mapped);
            }

            return BadRequest(new ErrorResource(skuResponse.Message));

        }

        /// <summary>
        /// Saves a new sku.
        /// </summary>
        /// <param name="resource">SKU data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPost("sku")]
        [ProducesResponseType(typeof(SKUResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSKUResource resource)
        {
            var sku = _mapper.Map<SaveSKUResource, SKU>(resource);
            var result = await _skuService.SaveAsync(sku);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var skuResource = _mapper.Map<SKU, SKUResource>(result.Resource);
            return Ok(skuResource);
        }

        /// <summary>
        /// Updates an existing sku according to an identifier.
        /// </summary>
        /// <param name="id">SKU identifier.</param>
        /// <param name="resource">Updated sku data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPut("sku/{id}")]
        [ProducesResponseType(typeof(SKUResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSKUResource resource)
        {
            var sku = _mapper.Map<SaveSKUResource, SKU>(resource);
            var result = await _skuService.UpdateAsync(id, sku);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var skuResource = _mapper.Map<SKU, SKUResource>(result.Resource);
            return Ok(skuResource);
        }

        /// <summary>
        /// Deletes a given sku according to an identifier.
        /// </summary>
        /// <param name="id">SKU identifier.</param>
        /// <returns>Response for the request.</returns>

        [HttpDelete("sku/{id}")]
        [ProducesResponseType(typeof(SKUResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _skuService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var skuResource = _mapper.Map<SKU, SKUResource>(result.Resource);
            return Ok(skuResource);
        }
    }
}
