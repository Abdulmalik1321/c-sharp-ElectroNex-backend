using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using BackendTeamwork.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class ProductsController : BaseController
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        // [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductJoinDto>> FindMany([FromQuery(Name = "limit")] int limit, [FromQuery(Name = "offset")] int offset, [FromQuery(Name = "sort")] SortBy sortBy, [FromQuery(Name = "searchTerm")] string searchTerm = "", string categoryFilter = null, string brandFilter = null)
        {
            return Ok(_productService.FindMany(limit, offset, sortBy, searchTerm, categoryFilter, brandFilter));
        }

        [HttpGet("{productId}")]
        // [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductJoinSingleDto>> FindOne(Guid productId)
        {
            return Ok(await _productService.FindOne(productId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductReadDto>> CreateOne([FromBody] ProductCreateDto newProduct)
        {
            return Ok(await _productService.CreateOne(newProduct));
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductReadDto?>> UpdateOne(Guid productId, [FromBody] ProductUpdateDto updatedProduct)
        {
            return Ok(await _productService.UpdateOne(productId, updatedProduct));
        }


        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductReadDto>> DeleteOne(Guid productId)
        {
            await _productService.DeleteOne(productId);
            return NoContent();
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductReadDto>> Search(string searchTerm)
        {
            return Ok(_productService.Search(searchTerm));
        }

    }
}
