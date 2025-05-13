using TestClean.Domain.Entities;

namespace TestClean.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);