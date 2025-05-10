using Microsoft.Extensions.DependencyInjection;
using TestClean.Application.Services.Authentication;

namespace TestClean.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
