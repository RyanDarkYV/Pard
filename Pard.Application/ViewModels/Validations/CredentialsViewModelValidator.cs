using FluentValidation;

namespace Pard.Application.ViewModels.Validations
{
    public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel>
    {        
        public CredentialsViewModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username can not be empty.");
            RuleFor(x => x.UserName).Length(4, 32).WithMessage("Username must be between 3 and 32 characters.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty.");
            RuleFor(x => x.Password).Length(6, 20).WithMessage("Password must be between 6 and 20 characters.");
        }
    }
}
