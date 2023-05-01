using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
