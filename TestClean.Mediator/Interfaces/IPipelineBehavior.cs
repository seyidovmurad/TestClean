namespace TestClean.Mediator.Interfaces;

public interface IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next);
}

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();
