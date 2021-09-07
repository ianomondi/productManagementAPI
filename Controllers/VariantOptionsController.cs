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
    public class VariantOptionsController : ControllerBase
    {
        private readonly IVariantOptionService _variantOptionService;
        private readonly IMapper _mapper;

        public VariantOptionsController(IVariantOptionService variantOptionService, IMapper mapper)
        {
            _variantOptionService = variantOptionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all categories.
        /// </summary>
        /// <returns>List os categories.</returns>
        [HttpGet("VariantOptions")]
        [ProducesResponseType(typeof(IEnumerable<VariantOptionResource>), 200)]
        public async Task<IEnumerable<VariantOptionResource>> ListAsync()
        {
            var categories = await _variantOptionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<VariantOption>, IEnumerable<VariantOptionResource>>(categories);

            return resources;
        }

        /// <summary>
        /// Saves a new variant option.
        /// </summary>
        /// <param name="resource">Variant Option data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPost("VariantOption")]
        [ProducesResponseType(typeof(VariantOptionResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveVariantOptionResource resource)
        {
            var variantOption = _mapper.Map<SaveVariantOptionResource, VariantOption>(resource);
            var result = await _variantOptionService.SaveAsync(variantOption);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var variantOptionResource = _mapper.Map<VariantOption, VariantOptionResource>(result.Resource);
            return Ok(variantOptionResource);
        }

        /// <summary>
        /// Updates an existing variant option according to an identifier.
        /// </summary>
        /// <param name="id">Variant option identifier.</param>
        /// <param name="resource">Updated variant option data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPut("VariantOption/{id}")]
        [ProducesResponseType(typeof(VariantOptionResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveVariantOptionResource resource)
        {
            var variantOption = _mapper.Map<SaveVariantOptionResource, VariantOption>(resource);
            var result = await _variantOptionService.UpdateAsync(id, variantOption);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var variantOptionResource = _mapper.Map<VariantOption, VariantOptionResource>(result.Resource);
            return Ok(variantOptionResource);
        }

        /// <summary>
        /// Deletes a given variant option according to an identifier.
        /// </summary>
        /// <param name="id">Variant option identifier.</param>
        /// <returns>Response for the request.</returns>

        [HttpDelete("VariantOption/{id}")]
        [ProducesResponseType(typeof(VariantOptionResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _variantOptionService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var variantOptionResource = _mapper.Map<VariantOption, VariantOptionResource>(result.Resource);
            return Ok(variantOptionResource);
        }
    }
}
