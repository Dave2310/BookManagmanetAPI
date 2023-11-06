
using DataAccess.Entities;

namespace BusinessLogic.Models.Reviews
{
    public class ReviewResponseModel : BaseEntity
    {
        public string ReviewerEmail { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
