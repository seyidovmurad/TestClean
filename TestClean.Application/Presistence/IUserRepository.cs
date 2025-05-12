using TestClean.Domain.Entities;

namespace TestClean.Application.Presistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}