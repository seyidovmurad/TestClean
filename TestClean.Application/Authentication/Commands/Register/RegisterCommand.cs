// using MediatR;
using TestClean.Application.Services.Authentication;
using TestClean.Mediator.Interfaces;

namespace TestClean.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<AuthenticationResult>;