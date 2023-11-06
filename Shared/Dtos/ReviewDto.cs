
using DataAccess.Entities;

namespace Shared.Dtos
{
    public record ReviewDto 
    {
        public Guid Id { get; set; }
        public string ReviewerEmail { get; set; }
        public double? Rating { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
