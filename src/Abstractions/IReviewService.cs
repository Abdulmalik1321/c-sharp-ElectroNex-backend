using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
        public interface IReviewService
        {
                public IEnumerable<ReviewReadDto> FindMany(Guid productId, int limit, int offset);

                public Task<ReviewReadDto?> FindOne(Guid id);

                public Task<ReviewReadDto> CreateOne(ReviewCreateDto newReview);

                public Task<ReviewReadDto?> UpdateOne(Guid reviewId, ReviewUpdateDto updatedReview);
                public Task<ReviewReadDto> DeleteOne(Review deleteReview);

        }
}