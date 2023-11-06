
using AutoMapper;
using BusinessLogic.Models.Categories;
using DataAccess.Entities;
using Shared.Dtos;

namespace BusinessLogic.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, CategoryResponseModel>();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryRequestModel, CategoryDto>();
            CreateMap<CategoryDto, CategoryRequestModel>();
        }
    }
}
