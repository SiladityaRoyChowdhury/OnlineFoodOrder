using Omf.ReviewManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Omf.ReviewManagementService.Data.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task AddReview(Review review);
        Task UpdateReview(Review review);
    }
}