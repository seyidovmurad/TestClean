using Microsoft.Extensions.DependencyInjection;
using TestClean.Application.Services.Authentication;
using TestClean.Application.Services.Authentication.Commands;
using TestClean.Application.Services.Authentication.Queries;

namespace TestClean.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        return services;
    }
}
