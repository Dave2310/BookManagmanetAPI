
using BusinessLogic.Models.Books;

namespace BusinessLogic.Services.BookService
{
    public interface IBookService 
    {
        Task CreateAndUploadToPdf(BookRequestModel model, string directoryPath);
        Task<BookResponseModel> GetById(Guid id);
        Task<BookResponseModel> GetByAuthor(string name);
        Task<List<BookResponseModel>> GetAll();
        Task<List<BookResponseModel>> GetPerPage(int page, int perPage);
        Task Update(BookRequestModel model);
        Task DeleteById(Guid id);
    }
}
