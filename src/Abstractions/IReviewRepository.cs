using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> FindMany(Guid productId, int limit, int offset);
        public Task<Review?> FindOne(Guid reviewId);

        public Task<Review> CreateOne(Review newReview);

        public Task<Review> UpdateOne(Review updatedReview);
        public Task<Review> DeleteOne(Review deleteReview);
    }
}