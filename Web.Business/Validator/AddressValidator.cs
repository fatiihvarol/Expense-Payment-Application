using FluentValidation;
using WebSchema;

namespace Web.Business.Validator
{
    public class AddressValidator : AbstractValidator<AddressRequest>
    {
        public AddressValidator() {

            RuleFor(x => x.Address1)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Address1 is required and should not exceed 100 characters.");

            RuleFor(x => x.Address2)
                .MaximumLength(100)
                .WithMessage("Address2 should not exceed 100 characters.");

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Country is required and should not exceed 50 characters.");

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("City is required and should not exceed 50 characters.");

            RuleFor(x => x.County)
                .MaximumLength(50)
                .WithMessage("County should not exceed 50 characters.");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("PostalCode is required and should not exceed 20 characters.");

            RuleFor(x => x.IsDefault)
                .NotNull()
                .WithMessage("IsDefault must be specified.");
        }
    }
}