using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class WishlistController : BaseController
    {

        private IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<WishlistReadDto>> FindMany(Guid userId, [FromQuery(Name = "limit")] int limit, [FromQuery(Name = "offset")] int offset)
        {
            return Ok(_wishlistService.FindMany(userId, limit, offset));
        }

        [HttpGet("{wishlistId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WishlistReadJoinDto>> FindOne(Guid wishlistId)
        {
            WishlistReadJoinDto? targetWishlist = await _wishlistService.FindOne(wishlistId);
            if (targetWishlist is not null)
            {
                return Ok(targetWishlist);
            }
            return NotFound();
        }

        [HttpPut("{wishlistId}/{productId}")]
        // [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishlistReadDto>> AddOneProduct(Guid wishlistId, Guid productId)
        {
            WishlistReadDto? targetWishlist = await _wishlistService.AddOneProduct(wishlistId, productId);
            if (targetWishlist is not null)
            {
                return Ok(targetWishlist);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<WishlistReadDto> CreateOne([FromBody] WishlistCreateDto newWishlist)
        {
            if (newWishlist is not null)
            {
                _wishlistService.CreateOne(newWishlist);
                return CreatedAtAction(nameof(CreateOne), newWishlist);
            }
            return BadRequest();
        }

        [HttpPut("{WishlistId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishlistReadDto?>> UpdateOne(Guid WishlistId, [FromBody] WishlistUpdateDto updatedWishlist)
        {
            WishlistReadDto? targetWishlist = await _wishlistService.UpdateOne(WishlistId, updatedWishlist);
            if (targetWishlist is not null)
            {
                return Ok(updatedWishlist);
            }
            return NotFound();
        }


        [HttpDelete("{wishlistId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishlistReadDto>> DeleteOne(Guid wishlistId)
        {
            WishlistReadDto? deletedWishlist = await _wishlistService.DeleteOne(wishlistId);
            if (deletedWishlist is not null)
            {
                return Ok(deletedWishlist);
            }
            return NotFound();
        }

    }
}
