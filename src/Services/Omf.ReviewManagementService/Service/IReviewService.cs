using Omf.ReviewManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Omf.ReviewManagementService.Service
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task<IEnumerable<Review>> SearchReview(int reviewid, string restaurantId, Guid userId);
        Task AddReview(Review review);
        Task UpdateReview(Review review);
    }
}