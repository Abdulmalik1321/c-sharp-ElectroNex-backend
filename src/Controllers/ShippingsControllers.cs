using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class ShippingController : BaseController
    {

        private IShippingService _shippingService;

        public ShippingController(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        [HttpGet("{shippingId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingReadDto?>> FindOne(Guid shippingId)
        {
            ShippingReadDto? shipping = await _shippingService.FindOne(shippingId);
            if (shipping is null) return BadRequest();

            return Ok(shipping);
        }

        [HttpGet("order/:{orderId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingReadDto?>> FindOneByOrderId(Guid orderId)
        {
            ShippingReadDto? shipping = await _shippingService.FindOneByOrderId(orderId);
            if (shipping is null) return BadRequest();

            return Ok(shipping);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShippingReadDto>> CreateOne([FromBody] ShippingCreateDto newShipping)
        {
            ShippingReadDto? shipping = await _shippingService.CreateOne(newShipping);
            if (shipping is null) return BadRequest();

            return Ok(shipping);
        }

        [HttpPut("{shippingId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingReadDto?>> UpdateOne(Guid shippingId, [FromBody] ShippingUpdateDto updatedShipping)
        {
            ShippingReadDto? shipping = await _shippingService.UpdateOne(shippingId, updatedShipping);
            if (shipping is null) return NotFound();
            return Ok(shipping);
        }

        [HttpDelete("{shippingId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingReadDto?>> DeleteOne(Guid shippingId)
        {
            ShippingReadDto? targetShipping = await _shippingService.DeleteOne(shippingId);
            if (targetShipping is null) return BadRequest();
            return NoContent();
        }

    }
}