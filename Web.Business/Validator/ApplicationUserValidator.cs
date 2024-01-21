using FluentValidation;
using WebSchema;

namespace Web.Business.Validator;

public class ApplicationUserValidator : AbstractValidator<ApplicationUserRequest>
{
    public ApplicationUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(50).WithName("username");
        
        RuleFor(x => x.Password).NotEmpty().MinimumLength(3).MaximumLength(50).Must(BeValidPassword)
            .WithMessage("Password must meet specific criteria.").WithName("password");
        
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(50).WithName("firstname");
        
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(50).WithName("lastname");
        
        RuleFor(x => x.Email).NotEmpty().MinimumLength(5).MaximumLength(50).WithName("email");
        RuleFor(x => x.Role).NotEmpty().MinimumLength(3).MaximumLength(50)
            .Must(BeValidRole).WithMessage("Invalid role. Allowed values are 'employee' or 'admin'").WithName("role");
    }

    private bool BeValidRole(string role)
    {
        return role == "employee" || role == "admin";
    }

    private bool BeValidPassword(string password)
    {
        // For example, ensuring it contains both letters and numbers
        return !string.IsNullOrWhiteSpace(password) && password.Any(char.IsLetter) && password.Any(char.IsDigit);
    }
}