
using AutoMapper;
using BusinessLogic.Models.Reviews;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using FluentValidation.Results;
using Shared.Dtos;

namespace BusinessLogic.Services.ReviewerService
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IRepository<Review> reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task Create(ReviewRequestModel model)
        {
            try
            {
                ReviewValidator reviewValidator = new ReviewValidator();

                ValidationResult validationResult = reviewValidator.Validate(model);

                if (!validationResult.IsValid)
                {
                    foreach (var failure in validationResult.Errors)
                    {
                        Console.WriteLine("Property " + failure.PropertyName + 
                            " failed validation. Error was: " + failure.ErrorMessage);
                    }
                }

                var reviewDto = _mapper.Map<ReviewDto>(model);

                var existingReveiewer = await _reviewRepository
                    .GetSingle(a => a.ReviewerEmail == reviewDto.ReviewerEmail);

                if(existingReveiewer != null)
                {
                    throw new Exception("With the specified email has already registered!");
                }

                existingReveiewer ??= new Review { ReviewerEmail = model.ReviewerEmail };

                var hasReviewed = _reviewRepository.Exist(existingReveiewer.Id);

                if (hasReviewed)
                {
                    throw new Exception("Reviewer has already reviewed this book.");
                }
                var reviewEntity = _mapper.Map<Review>(reviewDto);
                await _reviewRepository.Create(reviewEntity);
                await _reviewRepository.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeletById(Guid id)
        {
            try
            {
                var reviewEntity = _reviewRepository.GetById(id);

                if(reviewEntity == null)
                {
                    throw new Exception("Not Found");
                }

                var reviewDto = _mapper.Map<ReviewDto>(reviewEntity);
                var reviewModel = _mapper.Map<ReviewResponseModel>(reviewDto);

                _reviewRepository.DeleteById(id);
                await _reviewRepository.SaveChanges();
            }
            catch{ throw; }
        }

        public async Task<List<ReviewResponseModel>> GetAll()
        {
            try
            {
                var reviewEntity = await _reviewRepository.GetAll();
                var reviewDto = _mapper.Map<List<ReviewDto>>(reviewEntity);
                var reviewModel = _mapper.Map<List<ReviewResponseModel>>(reviewDto);

                return reviewModel;
            }
            catch{ throw; }
        }

        public async Task<ReviewResponseModel> GetById(Guid id)
        {
            try
            {
                var reviewEntity = await _reviewRepository.GetById(id);

                if(reviewEntity == null)
                {
                    throw new Exception("Invalid Id");
                }

                var reviewDto = _mapper.Map<ReviewDto>(reviewEntity);
                var reviewModel = _mapper.Map<ReviewResponseModel>(reviewDto);

                return reviewModel;
            }
            catch
            {
                throw;
            }
        }
    }
}
