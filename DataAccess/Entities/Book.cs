

namespace DataAccess.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid CategoryId { get;  set; }
        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
