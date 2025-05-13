using TestClean.Application.Common.Interfaces.Authentication;
using TestClean.Application.Presistence;
using TestClean.Domain.Entities;

namespace TestClean.Application.Services.Authentication.Queries;
public interface IAuthenticationQueryService
{
    AuthenticationResult Login(string email, string password);
}


public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // Check if the user exists
        var user = _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Check if the password is correct
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}
