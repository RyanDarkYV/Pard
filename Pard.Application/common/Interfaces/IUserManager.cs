using System.Threading.Tasks;
using Pard.Application.Models;
using Pard.Application.ViewModels;

namespace Pard.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<(Result Result, string UserId)> CreateUserAsync(RegistrationViewModel model);
    }
}