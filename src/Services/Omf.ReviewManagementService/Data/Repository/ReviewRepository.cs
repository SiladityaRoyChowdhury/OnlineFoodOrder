using Microsoft.EntityFrameworkCore;
using Omf.ReviewManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.ReviewManagementService.Data.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewContext _reviewContext;
        public ReviewRepository(ReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
            ((DbContext)reviewContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<Review>> GetAllReviews() => await _reviewContext.Reviews.ToListAsync();

        public async Task AddReview(Review review)
        {
            await _reviewContext.Reviews.AddAsync(review);
            await _reviewContext.SaveChangesAsync();
        }

        public async Task UpdateReview(Review review)
        {
            _reviewContext.Update(review);
            await _reviewContext.SaveChangesAsync();
        }
    }
}