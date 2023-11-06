using BookManagmanetAPI.Controllers;
using BusinessLogic.Models.Books;
using BusinessLogic.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookManagmentAPI.Test1
{
    public class BookServiceTest
    {
        [Fact]
        public async void CreateBook_ServiceThrowsException_ReturnsConflictResult()
        {
            // Arrange
            var model = new BookRequestModel();
            string directoryPath = string.Empty;
            var exceptionMessage = "An error occurred";

            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock
                .Setup(service => service.CreateAndUploadToPdf(model, directoryPath))
                .Throws(new Exception(exceptionMessage));

            var controller = new BookController(bookServiceMock.Object);

            // Act
            var result = await controller.CreateBook(model,directoryPath);

            // Assert
            bookServiceMock.Verify(); // Verify that the service method was called

            Assert.IsType<ConflictObjectResult>(result);
            var conflictResult = (ConflictObjectResult)result;

            Assert.Equal(exceptionMessage, conflictResult.Value);
        }

        [Fact]
        public async Task GetBookPerPage_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var page = 1;
            var perPage = 10;
            var expectedBooks = new List<BookResponseModel>(); // Provide sample books data here

            var bookServiceMock = new Mock<IBookService>();
             bookServiceMock
                .Setup(service => service.GetPerPage(page, perPage))
                .Returns(Task.FromResult(expectedBooks));

            var controller = new BookController(bookServiceMock.Object);

            // Act
            var result = await controller.GetBookPerPage(page, perPage);

            // Assert
            bookServiceMock.Verify(); // Verify that the service method was called

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;

            Assert.IsType<List<BookResponseModel>>(okResult.Value);
            var returnedBooks = (List<BookResponseModel>)okResult.Value;

            Assert.Equal(expectedBooks, returnedBooks);
        }

        [Fact]
        public async Task GetBookPerPage_ServiceThrowsException_ReturnsStatusCode406()
        {
            // Arrange
            var page = 1;
            var perPage = 10;
            var exceptionMessage = "An error occurred";

            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock
                .Setup(service => service.GetPerPage(page, perPage))
                .Throws(new Exception(exceptionMessage));

            var controller = new BookController(bookServiceMock.Object);

            // Act
            var result = await controller.GetBookPerPage(page, perPage);

            // Assert
            bookServiceMock.Verify(); // Verify that the service method was called

            Assert.IsType<ObjectResult>(result);
            var objectResult = (ObjectResult)result;

            Assert.Equal(406, objectResult.StatusCode);
            Assert.Equal(exceptionMessage, objectResult.Value);
        }
    }
}
