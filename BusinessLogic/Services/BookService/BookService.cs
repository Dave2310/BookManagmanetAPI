
using AutoMapper;
using BusinessLogic.Models.Books;
using DataAccess.Entities;
using DataAccess.Repositories.BookRepository;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.exceptions;
using Shared.Dtos;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task CreateAndUploadToPdf(BookRequestModel model,string directoryPath)
        {
            try
            {
                var bookDto = _mapper.Map<BookDto>(model);
                var bookEntity = _mapper.Map<Book>(bookDto);
                await _bookRepository.Create(bookEntity);

                 directoryPath = _configuration["PdfSettings:DirectoryPath"];
                // Check if the directory exists, and create it if it doesn't
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                Document document = new Document();

                // Create a new PdfWriter instance with the full file path
                PdfWriter writer = PdfWriter.GetInstance(
                    document, new FileStream(Path.Combine(directoryPath, "Book Details.pdf"), FileMode.Create));

                // Open the document
                document.Open();

                // Add album data to the document
                Paragraph title = new Paragraph("Book Details");
                title.Font.Size = 16;
                document.Add(title);

                document.Add(new Paragraph($"Author: {bookDto.Author}"));
                document.Add(new Paragraph($"Title: {bookDto.Title}"));
                document.Add(new Paragraph($"Content: {bookDto.Content}"));

                // Close the document
                document.Close();



                await _bookRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidPdfException(ex.Message);
            }

        }

        public async Task DeleteById(Guid id)
        {
            try
            {
                var bookEntity = await _bookRepository.GetById(id);

                if (bookEntity == null)
                {
                    throw new Exception("Not Found");
                }

                var bookModel = _mapper.Map<BookResponseModel>(bookEntity);
                _bookRepository.DeleteById(id);
                await _bookRepository.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<BookResponseModel>> GetAll()
        {
            try
            {
                var bookEntity = await _bookRepository.GetAll();
                var bookModel = _mapper.Map<List<BookResponseModel>>(bookEntity);
                return bookModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<BookResponseModel> GetByAuthor(string name)
        {
            try
            {
                var bookEntity = await _bookRepository.GetByAuthor(name);

                if (bookEntity == null)
                {
                    throw new Exception("Not Found");
                }

                var bookModel = _mapper.Map<BookResponseModel>(bookEntity);
                return bookModel;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookResponseModel> GetById(Guid id)
        {
            try
            {
                var bookEntity = await _bookRepository.GetById(id);

                if (bookEntity == null)
                {
                    throw new Exception("Invalid Id");
                }

                var bookDto = _mapper.Map<BookDto>(bookEntity);
                var bookModel = _mapper.Map<BookResponseModel>(bookDto);

                return bookModel;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookResponseModel>> GetPerPage(int page, int perPage)
        {
            try
            {
                var booksEntity = await _bookRepository.GetPerPage(page, perPage);

                if (booksEntity == null)
                {
                    throw new Exception("Not Found");
                }

                var booksDto = _mapper.Map<List<BookDto>>(booksEntity);
                var bookModel = _mapper.Map<List<BookResponseModel>>(booksDto);

                return bookModel;
            }
            catch(Exception ex)
            {
                throw new InvalidDataException(ex.Message);
            }
        }

        public async Task Update(BookRequestModel model)
        {
            try
            {
                var bookEntity = _mapper.Map<Book>(model);
                await _bookRepository.Update(bookEntity);
                await _bookRepository.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
