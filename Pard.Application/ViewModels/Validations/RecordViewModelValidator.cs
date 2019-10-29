using FluentValidation;

namespace Pard.Application.ViewModels.Validations
{
    public class RecordViewModelValidator : AbstractValidator<RecordViewModel>
    {
        public RecordViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title can not be empty.");
            RuleFor(x => x.Title).Length(0, 200).WithMessage("Title must be between 0 and 200 characters.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty.");
            RuleFor(x => x.Description).Length(0, 1000).WithMessage("Title must be between 0 and 1000 characters.");

            RuleFor(x => x.UserId).NotNull().WithMessage("UserId cannot be null.");

            RuleFor(x => x.IsDone).Must(x => x == false || x == true);
        }
    }
}