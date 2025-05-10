using Microsoft.Extensions.DependencyInjection;
using TestClean.Application.Common.Interfaces.Authentication;
using TestClean.Application.Common.Interfaces.Services;
using TestClean.Infrastructure.Authentication;
using TestClean.Infrastructure.Services;

namespace TestClean.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
