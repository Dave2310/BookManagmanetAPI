using BusinessLogic.Models.Reviews;
using BusinessLogic.Services.ReviewerService;
using Microsoft.AspNetCore.Mvc;

namespace BookManagmanetAPI.Controllers
{ 
    [Route("api/[controller]/[action]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestModel model)
        {
            try
            {
                await _reviewService.Create(model);
                return Ok("Success!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            try
            {
                var review = await _reviewService.GetById(id);
                return Accepted(review);
            }
            catch(Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }
    }
}
