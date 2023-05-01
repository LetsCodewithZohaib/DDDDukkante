using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dukkantek.Inventory.Persistence.Extensions
{
    public static class Extensions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

    }
}
