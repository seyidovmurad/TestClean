using Microsoft.Extensions.DependencyInjection;
using TestClean.Mediator.Interfaces;

namespace TestClean.Mediator.Implementation;


public class Sender : ISender
{
    private readonly IServiceProvider _provider;

    public Sender(IServiceProvider provider)
    {
        _provider = provider;
    }

    // public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    // {
    //     var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
    //     dynamic handler = _provider.GetService(handlerType);
    //     if (handler == null)
    //     {
    //         throw new InvalidOperationException($"Handler for {request.GetType().Name} not found.");
    //     }

    //     return handler.Handle((dynamic)request, cancellationToken);
    // }

     public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        dynamic handler = _provider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException($"Handler for {requestType.Name} not found.");

        // Wrap the actual handler invocation
        RequestHandlerDelegate<TResponse> handlerDelegate = () =>
            (Task<TResponse>)handlerType.GetMethod("Handle")!.Invoke(handler, new object[] { request, cancellationToken })!;

        // Resolve all applicable pipeline behaviors
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
        RequestHandlerDelegate<TResponse> pipeline  = (dynamic)_provider.GetServices(behaviorType)
            .Cast<dynamic>() // Cast to dynamic to invoke `Handle` method
            .Reverse() // Reverse to build pipeline inside-out
            .Aggregate(handlerDelegate, (next, behavior) =>
                new RequestHandlerDelegate<TResponse>(() =>
                    behavior.Handle((dynamic)request, cancellationToken, next)));

        return pipeline();
    }
}