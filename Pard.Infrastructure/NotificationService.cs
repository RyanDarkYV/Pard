using System.Threading.Tasks;
using Pard.Application.Common.Interfaces;
using Pard.Application.Models;

namespace Pard.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }
    }
}