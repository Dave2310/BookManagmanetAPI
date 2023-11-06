
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;

namespace DataAccess.Repositories.BookRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IReadOnlyList<Book>> GetPerPage(int perPage, int page);
        Task<Book> GetByAuthor(string author);
    }
}
