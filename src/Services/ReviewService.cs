using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Services
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository _reviewRepository;
        private IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public IEnumerable<ReviewReadDto> FindMany(Guid productId, int limit, int offset)
        {
            return _reviewRepository.FindMany(productId, limit, offset).Select(_mapper.Map<ReviewReadDto>);
        }
        public async Task<ReviewReadDto?> FindOne(Guid reviewId)
        {
            return _mapper.Map<ReviewReadDto>(await _reviewRepository.FindOne(reviewId));
        }
        public async Task<ReviewReadDto> CreateOne(ReviewCreateDto newReview)
        {
            return _mapper.Map<ReviewReadDto>(await _reviewRepository.CreateOne(_mapper.Map<Review>(newReview)));
        }
        public async Task<ReviewReadDto?> UpdateOne(Guid reviewId, ReviewUpdateDto updatedReview)
        {
            ReviewReadDto? targetReview = _mapper.Map<ReviewReadDto>(await _reviewRepository.FindOne(reviewId));
            if (targetReview is null)
            {
                return null;
            }
            Review review = _mapper.Map<Review>(updatedReview);
            review.Id = reviewId;
            return _mapper.Map<ReviewReadDto>(await _reviewRepository.UpdateOne(review));
        }
        public async Task<ReviewReadDto> DeleteOne(Review deleteReview)
        {
            return _mapper.Map<ReviewReadDto>(await _reviewRepository.DeleteOne(deleteReview));
        }


    }
}