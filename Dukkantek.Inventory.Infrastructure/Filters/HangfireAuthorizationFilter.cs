using Hangfire.Dashboard;

namespace Dukkantek.Inventory.Infrastructure.Filters
{
     public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
     {
          public bool Authorize(DashboardContext context)
          {
               var httpContext = context.GetHttpContext();
               // Allow all authenticated users to see the Dashboard (potentially dangerous).
               return httpContext.User.Identity.IsAuthenticated;
          }
     }
}