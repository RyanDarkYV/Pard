using System.Threading.Tasks;
using Pard.Application.Models;

namespace Pard.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(MessageDto message);
    }
}