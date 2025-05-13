

using MediatR;
using TestClean.Application.Services.Authentication;

namespace Namespace.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>;