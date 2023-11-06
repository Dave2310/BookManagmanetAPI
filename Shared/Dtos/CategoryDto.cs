
using DataAccess.Entities;

namespace Shared.Dtos
{
    public record CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
