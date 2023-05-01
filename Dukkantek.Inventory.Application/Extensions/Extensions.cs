using Dukkantek.Inventory.Application.Behaviors.Loggings;
using Dukkantek.Inventory.Application.Behaviors.Validations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dukkantek.Inventory.Application.Extensions
{
    public static class Extensions
    {
        public static void AddMediatRBehaviors(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //Todo: Add other pipeline behaviors here
        }
    }
}
