using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestClean.Mediator.Implementation;

namespace TestClean.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddMediator(typeof(DependencyInjection).Assembly);
        return services;
    }
}
