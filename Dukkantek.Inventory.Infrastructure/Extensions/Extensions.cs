using Hangfire;
using Hangfire.SqlServer;
using Dukkantek.Inventory.Application.Interfaces.Infrastructure;
using Dukkantek.Inventory.Infrastructure.Filters;
using Dukkantek.Inventory.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Dukkantek.Inventory.Infrastructure.Extensions
{
     public static class Extensions
     {
          public static void AddInfrastructureService(this IServiceCollection services, IConfiguration Configuration)
          {
               services.AddTransient<IEmailService, EmailService>();
               services.AddTransient<ISmsService, SmsService>();
          }

          public static void AddHangFireService(this IServiceCollection services, IConfiguration Configuration)
          {
               services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection.Jobs"), new SqlServerStorageOptions
                    {
                         CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                         SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                         QueuePollInterval = TimeSpan.Zero,
                         UseRecommendedIsolationLevel = true,
                         UsePageLocksOnDequeue = true,
                         DisableGlobalLocks = true
                    }));

               //services.AddTransient<ISubscriptionService, SubscriptionService>();

               services.AddHangfireServer();
          }

          public static IApplicationBuilder UseHangfireService(this IApplicationBuilder app, IWebHostEnvironment env)
          {
               if (env.IsDevelopment() || env.IsEnvironment("Test"))
               {
                    app.UseHangfireDashboard("/hangfire", new DashboardOptions
                    {
                         DashboardTitle = "Dukkantek.Inventory JOBS",
                    });
               }
               else
               {
                    app.UseHangfireDashboard("/hangfire", new DashboardOptions
                    {
                         DashboardTitle = "Dukkantek.Inventory JOBS",
                         Authorization = new[] { new HangfireAuthorizationFilter() }
                    });
               }

               //RecurringJob.AddOrUpdate<ISubscriptionService>(nameof(SubscriptionService.ProcessTodayDueSubscription), e => e.ProcessTodayDueSubscription(), Cron.Daily(1));
               //RecurringJob.AddOrUpdate<ISubscriptionService>(nameof(SubscriptionService.ProcessSubscriptionDueNextWeek), e => e.ProcessSubscriptionDueNextWeek(), Cron.Daily(1));

               return app;
          }
     }
}
