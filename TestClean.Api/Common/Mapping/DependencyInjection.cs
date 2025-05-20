using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using TestClean.Application.Common.Interfaces.Authentication;
using TestClean.Application.Common.Interfaces.Services;
using TestClean.Application.Presistence;
using TestClean.Infrastructure.Authentication;
using TestClean.Infrastructure.Presistence;
using TestClean.Infrastructure.Services;

namespace TestClean.Api.Common.Mapping;
public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
