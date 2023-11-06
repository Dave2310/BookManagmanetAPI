
using AutoMapper;
using BusinessLogic.Models.Reviews;
using DataAccess.Entities;
using Shared.Dtos;

namespace BusinessLogic.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, ReviewResponseModel>();

            CreateMap<ReviewRequestModel, ReviewDto>().ReverseMap();
            CreateMap<ReviewDto, Review>().ReverseMap();
        }
    }
}
