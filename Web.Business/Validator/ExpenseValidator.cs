using FluentValidation;
using WebSchema;

namespace Web.Business.Validator
{
    public class ExpenseValidator : AbstractValidator<ExpenseRequest>
    {
        public ExpenseValidator()
        {

            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(BeValidDate)
                .WithMessage("Invalid Date.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Description is required and should not exceed 255 characters.");

            RuleFor(x => x.Document)
                .MaximumLength(255)
                .WithMessage("Document path should not exceed 255 characters.");

            RuleFor(x => x.PaymentRequestType)
                .NotEmpty()
                .MaximumLength(50)
                .Must(BeValidPaymentRequestType)
                .WithMessage("PaymentRequestType must be either 'EFT' or 'HAVALE' with capital letter.");

            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator()); 

        }

        private bool BeValidDate(DateTime date)
        {
            // For example, ensuring it is not in the future
            return date <= DateTime.Now;
        }
        private bool BeValidPaymentRequestType(string paymentRequestType)
        {
            return paymentRequestType == "EFT" || paymentRequestType == "HAVALE";
        }
    }
}