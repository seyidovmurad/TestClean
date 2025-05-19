
using TestClean.Application.Services.Authentication;
using TestClean.Mediator.Interfaces;

namespace Namespace.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>;