
using AutoMapper;
using BusinessLogic.Models.Books;
using BusinessLogic.Models.Categories;
using DataAccess.Entities;
using Shared.Dtos;

namespace BusinessLogic.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, BookResponseModel>();

            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<BookRequestModel, BookDto>();
            CreateMap<BookDto, BookRequestModel>();
        }
    }
}
