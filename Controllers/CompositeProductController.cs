using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Models.Queries;
using ProductManagement.API.Resources;
using Supermarket.API.Domain.Models.Queries;
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
    public class CompositeProductController : ControllerBase
    {
        private readonly ICompositeProductService _compositeProductService;
        private readonly IMapper _mapper;

        public CompositeProductController(ICompositeProductService compositeProductService, IMapper mapper)
        {
            _compositeProductService = compositeProductService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all compositeProducts.
        /// </summary>
        /// <returns>List os compositeProducts.</returns>
        [HttpGet("compositeProducts/{id}")]
        [ProducesResponseType(typeof(QueryResultResource<CompositeProductResource>), 200)]
        public async Task<QueryResultResource<CompositeProductResource>> ListAsync(int id)
        {
            CompositeProductsQueryResource query = new CompositeProductsQueryResource() { ProductId = id };

            var compositeProductsQuery = _mapper.Map<CompositeProductsQueryResource, CompositeProductsQuery>(query);

            var compositeProducts = await _compositeProductService.ListAsync(compositeProductsQuery);
            var resources = _mapper.Map<QueryResult<CompositeProduct>, QueryResultResource<CompositeProductResource>>(compositeProducts);

            return resources;
        }

        /// <summary>
        /// Saves a new category.
        /// </summary>
        /// <param name="resource">CompositeProduct data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPost("compositeProduct")]
        [ProducesResponseType(typeof(CompositeProductResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCompositeProductResource resource)
        {
            var category = _mapper.Map<SaveCompositeProductResource, CompositeProduct>(resource);
            var result = await _compositeProductService.SaveAsync(category);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var categoryResource = _mapper.Map<CompositeProduct, CompositeProductResource>(result.Resource);
            return Ok(categoryResource);
        }

        /// <summary>
        /// Updates an existing category according to an identifier.
        /// </summary>
        /// <param name="id">CompositeProduct identifier.</param>
        /// <param name="resource">Updated category data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPut("compositeProduct/{id}")]
        [ProducesResponseType(typeof(CompositeProductResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCompositeProductResource resource)
        {
            var category = _mapper.Map<SaveCompositeProductResource, CompositeProduct>(resource);
            var result = await _compositeProductService.UpdateAsync(id, category);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var categoryResource = _mapper.Map<CompositeProduct, CompositeProductResource>(result.Resource);
            return Ok(categoryResource);
        }

        /// <summary>
        /// Deletes a given category according to an identifier.
        /// </summary>
        /// <param name="id">CompositeProduct identifier.</param>
        /// <returns>Response for the request.</returns>

        [HttpDelete("compositeProduct/{id}")]
        [ProducesResponseType(typeof(CompositeProductResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _compositeProductService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var categoryResource = _mapper.Map<CompositeProduct, CompositeProductResource>(result.Resource);
            return Ok(categoryResource);
        }
    }
}
