using TestClean.Api.Common.Mapping;

namespace TestClean.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMapping();

        return services;
    }
}