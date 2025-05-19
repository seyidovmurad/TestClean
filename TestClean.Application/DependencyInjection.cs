
using Microsoft.Extensions.DependencyInjection;
using TestClean.Application.Pipelines;
using TestClean.Mediator.Implementation;
using TestClean.Mediator.Interfaces;

namespace TestClean.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

        // services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddMediator(typeof(DependencyInjection).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}
