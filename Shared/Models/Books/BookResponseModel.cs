
using DataAccess.Entities;

namespace BusinessLogic.Models.Books
{
    public class BookResponseModel : BaseEntity
    {
        public int Guid  { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
