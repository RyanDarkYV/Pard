using FluentValidation;

namespace Pard.Application.ViewModels.Validations
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email can not be empty.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Enter valid email.");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName cannot be empty");

            RuleFor(x => x.Login).NotEmpty().WithMessage("Username can not be empty.");
            RuleFor(x => x.Login).Length(4, 32).WithMessage("Username must be between 3 and 32 characters.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty.");
            RuleFor(x => x.Password).Length(6, 20).WithMessage("Password must be between 6 and 20 characters.");

        }
    }
}