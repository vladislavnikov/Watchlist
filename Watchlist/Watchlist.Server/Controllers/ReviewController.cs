using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Core.Common;
using Watchlist.Core.Contracts;
using Watchlist.Core.DTOs.Review;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IMapper mapper;
        private readonly IReviewRepository reviewRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(IMapper _mapper, IReviewRepository _reviewRepository, UserManager<ApplicationUser> _userManager)
        {
            this.mapper = _mapper;
            this.reviewRepository = _reviewRepository;
            this.userManager = _userManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviewsOfMovie(int movieId)
        {
            var reviews = reviewRepository.GetMediaItemReviews(movieId);
            var revieDtos = mapper.Map<List<ReviewDto>>(reviews);

            if (revieDtos == null)
            {
                return BadRequest(Messages.NoReviews);
            }

            return Ok(revieDtos);
        }

        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto reviewModel)
        {
            if (reviewModel == null)
            {
                return BadRequest(ModelState);
            }

            var review = mapper.Map<Review>(reviewModel);
            await reviewRepository.CreateReviewAsync(review);

            return Ok();
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var review = mapper.Map<Review>(updatedReview);
            await reviewRepository.UpdateReviewAsync(reviewId, review);

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await reviewRepository.DeleteReviewAsync(reviewId);
            return NoContent();
        }
    }
}
