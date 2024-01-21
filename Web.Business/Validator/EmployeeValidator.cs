using FluentValidation;
using WebSchema;

namespace Web.Business.Validator
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeValidator()
        {

            RuleFor(x => x.IdentityNumber)
                .NotEmpty()
                .MinimumLength(11)
                .MaximumLength(11)
                .Matches("^[0-9]+$")
                .WithMessage("IdentityNumber must be a numeric string of length 11.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .Must(BeValidDateOfBirth)
                .WithMessage("Invalid DateOfBirth.");

            RuleFor(x => x.IBAN)
                .NotEmpty()
                .Matches("^[A-Z]{2}[0-9]{2}[A-Z0-9]{1,30}$")
                .WithMessage("Invalid IBAN format.");
        }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            return dateOfBirth <= DateTime.Now;
        }
    }
}