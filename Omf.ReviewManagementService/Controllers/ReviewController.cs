using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Omf.ReviewManagementService.Service;
using Omf.ReviewManagementService.DomainModel;
using System.Linq;

namespace Omf.ReviewManagementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IReviewService _reviewService;

        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [HttpGet(Name ="Get")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while fetching review", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("Search/reviewId/{reviewId}/restaurantId/{restaurantId}/userId/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> SearchReview(int reviewId, string restaurantId, Guid userId)
        {
            try
            {
                var reviews = await _reviewService.SearchReview(reviewId, restaurantId, userId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while fetching review", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            _logger.LogInformation("Adding new review");

            try
            {
                await _reviewService.AddReview(review);
                _logger.LogInformation("Review added to database Successfully");
                return CreatedAtRoute("Get", review);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while adding new review", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateReview([FromBody] Review review)
        {
            _logger.LogInformation("Updating review : " + review.ReviewId);

            try
            {
                var data = await _reviewService.SearchReview(review.ReviewId, null, review.UserId);
                if (data.Any())
                {
                    await _reviewService.UpdateReview(review);
                    _logger.LogInformation("Review Updated to database Successfully");
                    return CreatedAtRoute("Get", review);
                }
                _logger.LogInformation("Review details not found");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while updating order", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }
    }
}
