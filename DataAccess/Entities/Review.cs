
namespace DataAccess.Entities
{
    public class Review : BaseEntity
    {
        public string ReviewerEmail { get; set; }
        public double? Rating { get; set; }
        public string? Comment { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
