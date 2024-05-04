
using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private DbSet<Review> _reviews;
        private DatabaseContext _databaseContext;
        public ReviewRepository(DatabaseContext databaseContext)
        {
            _reviews = databaseContext.Review;
            _databaseContext = databaseContext;
        }

        public IEnumerable<Review> FindMany()
        {
            return _reviews;
        }
        public async Task<Review?> FindOne(Guid reviewId)
        {
            return await _reviews.AsNoTracking().FirstOrDefaultAsync(review => review.Id == reviewId);
        }
        public async Task<Review> CreateOne(Review newReview)
        {
            await _reviews.AddAsync(newReview);
            await _databaseContext.SaveChangesAsync();
            return newReview;

        }
        public async Task<Review> UpdateOne(Review updatedReview)
        {
            _reviews.Update(updatedReview);
            await _databaseContext.SaveChangesAsync();
            return updatedReview;
        }

        public async Task<Review> DeleteOne(Review deleteReview)
        {
            _reviews.Remove(deleteReview);
            await _databaseContext.SaveChangesAsync();
            return deleteReview;
        }

    }
}
