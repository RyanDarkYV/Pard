using FluentValidation.Attributes;
using Pard.Application.ViewModels.Validations;

namespace Pard.Application.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}