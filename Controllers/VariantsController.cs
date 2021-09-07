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
    public class VariantsController : ControllerBase
    {

        private readonly IVariantService _variantService;
        private readonly IMapper _mapper;

        public VariantsController(IVariantService variantService, IMapper mapper)
        {
            _variantService = variantService;
            _mapper = mapper;
        }

        /// <summary>   
        /// Lists all variants.
        /// </summary>
        /// <returns>List os variants.</returns>
        [HttpGet("variants")]
        [ProducesResponseType(typeof(IEnumerable<VariantResource>), 200)]
        public async Task<IEnumerable<VariantResource>> ListAsync()
        {
            var variants = await _variantService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Variant>, IEnumerable<VariantResource>>(variants);

            return resources;
        }

        /// <summary>
        /// Saves a new variant.
        /// </summary>
        /// <param name="resource">Variant data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPost("variant")]
        [ProducesResponseType(typeof(VariantResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveVariantResource resource)
        {
            var variant = _mapper.Map<SaveVariantResource, Variant>(resource);
            var result = await _variantService.SaveAsync(variant);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var variantResource = _mapper.Map<Variant, VariantResource>(result.Resource);
            return Ok(variantResource);
        }


        /// <summary>
        /// Updates an existing variant according to an identifier.
        /// </summary>
        /// <param name="id">Variant identifier.</param>
        /// <param name="resource">Updated variant data.</param>
        /// <returns>Response for the request.</returns>

        [HttpPut("variant/{id}")]
        [ProducesResponseType(typeof(VariantResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveVariantResource resource)
        {
            var variant = _mapper.Map<SaveVariantResource, Variant>(resource);
            var result = await _variantService.UpdateAsync(id, variant);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var variantResource = _mapper.Map<Variant, VariantResource>(result.Resource);
            return Ok(variantResource);
        }

        /// <summary>
        /// Deletes a given variant according to an identifier.
        /// </summary>
        /// <param name="id">Variant identifier.</param>
        /// <returns>Response for the request.</returns>

        [HttpDelete("variant/{id}")]
        [ProducesResponseType(typeof(VariantResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _variantService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var variantResource = _mapper.Map<Variant, VariantResource>(result.Resource);
            return Ok(variantResource);
        }

    }
}
