using Omf.ReviewManagementService.Data.Repository;
using Omf.ReviewManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.ReviewManagementService.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task AddReview(Review review) => _reviewRepository.AddReview(review);

        public Task UpdateReview(Review review) => _reviewRepository.UpdateReview(review);

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviews();
            return reviews;
        }

        public async Task<IEnumerable<Review>> SearchReview(int reviewid, string restaurantId, Guid userId)
        {
            var reviews = await _reviewRepository.GetAllReviews();

            var filteredReviews = from r in reviews
                                    where (restaurantId == null || r.RestaurantId.ToLower() == restaurantId.ToLower())
                                    && (reviewid == 0 || r.ReviewId == reviewid)
                                    && (userId == null || r.UserId == userId)
                                    select r;

            return filteredReviews;
        }
    }
}