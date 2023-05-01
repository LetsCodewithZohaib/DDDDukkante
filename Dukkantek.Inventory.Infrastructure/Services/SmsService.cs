using Dukkantek.Inventory.Application.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        public IConfiguration Configuration { get; }
        public SmsService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            List<string> telNumbers = new List<string>();

            if (phoneNumber.StartsWith('0') && phoneNumber.Length.Equals(11))
            {
                phoneNumber = phoneNumber.Substring(1, 10);
                phoneNumber = "+971" + phoneNumber;
            }
            else if (!phoneNumber.StartsWith('0') && phoneNumber.Length.Equals(10))
            {
                phoneNumber = "+971" + phoneNumber;
            }
            telNumbers.Add(phoneNumber);
        }
    }
}