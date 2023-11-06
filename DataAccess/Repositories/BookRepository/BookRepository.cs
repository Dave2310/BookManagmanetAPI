
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.BookRepository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookContext context) : base(context)
        { }

        public async Task<Book> GetByAuthor(string author)
        {
            return await _dbSet.FirstOrDefaultAsync(name => name.Author == author);
        }

        public async Task<IReadOnlyList<Book>> GetPerPage(int perPage, int page)
        {
            var books =  await _dbSet.Include(b => b.Category)
            .Include(b => b.Reviews)
            .Skip(perPage * (page - 1))
            .Take(perPage)
            .ToListAsync();

            foreach (var book in books)
            {
                double? averageRating = book.Reviews?.Average(review => review.Rating);
            }

            return books;
        }
    }
}
