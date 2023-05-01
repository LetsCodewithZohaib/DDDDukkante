using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Interfaces.Infrastructure
{
    public interface ISmsService
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}
