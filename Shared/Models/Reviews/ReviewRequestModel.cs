
namespace BusinessLogic.Models.Reviews
{
    public class ReviewRequestModel
    {
        public string ReviewerEmail { get; set; }
        public int? Rating { get; set; }
        public string Comment { get; set; }
        public Guid BookId { get; set; }
    }
}
