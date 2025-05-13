using TestClean.Mediator.Interfaces;

namespace TestClean.Mediator.Implementation;


public class Sender : ISender
{
    private readonly IServiceProvider _provider;

    public Sender(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = _provider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for {request.GetType().Name} not found.");
        }

        return handler.Handle((dynamic)request, cancellationToken);
    }
}