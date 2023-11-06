
using BusinessLogic.Models.Reviews;
using FluentValidation;

namespace BusinessLogic
{
    public class ReviewValidator : AbstractValidator<ReviewRequestModel>
    {
        public ReviewValidator()
        {
            RuleFor(a => a.ReviewerEmail).NotEmpty().EmailAddress();
            RuleFor(a => a.Rating).NotEmpty().InclusiveBetween(1,5);  
        }
    }
}
