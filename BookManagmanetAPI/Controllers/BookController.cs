using BusinessLogic.Models.Books;
using BusinessLogic.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookManagmanetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            try
            {
                var book = await _bookService.GetById(id);
                return Accepted(book);
            }
            catch (Exception ex)
            {
                return StatusCode(404,ex.Message);
            }
        }

        [HttpGet("{page},{perPage}")]
        public async Task<IActionResult> GetBookPerPage(int page, int perPage)
        {
            try
            {
                var books = await _bookService.GetPerPage(page, perPage);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(406,ex.Message);
            }
        }

        [HttpGet("{author}")]
        public async Task<IActionResult> GetBookByAuthor(string author)
        {
            try
            {
               var book = await _bookService.GetByAuthor(author);
                return Ok(book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _bookService.GetAll();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] BookRequestModel model,string directoryPath)
        {
            try
            {
                await _bookService.CreateAndUploadToPdf(model,directoryPath);

                byte[] fileBytes = await System.IO.File
                    .ReadAllBytesAsync(directoryPath);

                return File(fileBytes, "application/pdf", "Book Details.pdf");
            }
            catch(Exception ex)
            {
                return CreatedAtAction("GetBook", ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] BookRequestModel model)
        {
 
                await _bookService.Update(model);
                return CreatedAtAction("Data has been successfully updated!",model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookById(Guid id)
        {
            try
            {
                 await _bookService.DeleteById(id);
                return Ok("Data has been successfully deleted!");
            }
            catch
            {
                return NoContent();
            }
        }
    }
}
