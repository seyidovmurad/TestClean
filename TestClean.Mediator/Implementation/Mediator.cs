
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TestClean.Mediator.Interfaces;

namespace TestClean.Mediator.Implementation;

public static class Mediator
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();
        
        services.AddScoped<ISender, Sender>();
        
        var handlerInterfaceType = typeof(IRequestHandler<,>);

        var handlerTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                    .Select(i => new { Interfaces = i, Implementation = t}));
            
        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.Interfaces, handler.Implementation);
        }

        return services;
    }
}