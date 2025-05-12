using TestClean.Domain.Entities;

namespace TestClean.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
