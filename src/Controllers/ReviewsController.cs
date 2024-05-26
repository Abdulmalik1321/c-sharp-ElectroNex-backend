using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class ReviewsController : BaseController
    {

        private IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("product/{productId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ReviewReadDto>> FindMany(Guid productId, [FromQuery(Name = "limit")] int limit, [FromQuery(Name = "offset")] int offset)
        {
            return Ok(_reviewService.FindMany(productId, limit, offset));
        }

        [HttpGet("{reviewId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewReadDto?>> FindOne(Guid reviewId)
        {
            return Ok(await _reviewService.FindOne(reviewId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewReadDto>> CreateOne([FromBody] ReviewCreateDto newReview)
        {
            ReviewReadDto? review = await _reviewService.CreateOne(newReview);
            if (review is not null)
            {
                return Ok(review);
            }
            return NotFound();
        }

        [HttpPut("{reviewId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewReadDto?>> UpdateOne(Guid reviewId, [FromBody] ReviewUpdateDto updatedReview)
        {
            ReviewReadDto? review = await _reviewService.UpdateOne(reviewId, updatedReview);
            if (review is not null)
            {
                return Ok(review);
            }
            return NotFound();
        }

    }
}