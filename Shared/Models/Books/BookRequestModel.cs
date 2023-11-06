
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models.Books
{
    public class BookRequestModel
    {
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }

        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
