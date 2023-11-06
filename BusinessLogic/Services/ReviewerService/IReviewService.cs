
using BusinessLogic.Models.Reviews;

namespace BusinessLogic.Services.ReviewerService
{
    public interface IReviewService
    {
        Task Create(ReviewRequestModel model);
        Task<ReviewResponseModel> GetById(Guid id);
        Task<List<ReviewResponseModel>> GetAll();
        Task DeletById(Guid id);
    }
}
