using FluentValidation;
using WebSchema;

namespace Web.Business.Validator
{
    public class ExpenseCategoryValidator : AbstractValidator<ExpenseCategoryRequest>
    {
        public ExpenseCategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("CategoryName is required and should not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Description should not exceed 255 characters.");
        }
    }
}