using System.Threading.Tasks;
using Pard.Application.Models;
using Pard.Application.ViewModels;

namespace Pard.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<(Result Result, string JwtToken)> Login(CredentialsViewModel model);
    }
}