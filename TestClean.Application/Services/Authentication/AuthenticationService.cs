
using TestClean.Application.Common.Interfaces.Authentication;
using TestClean.Application.Presistence;
using TestClean.Domain.Entities;

namespace TestClean.Application.Services.Authentication;
public interface IAuthenticationService
{
    AuthenticationResult Login(string email, string password);
    AuthenticationResult Register(string name, string surname, string email, string password);

}


public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
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

    public AuthenticationResult Register(string name, string surname, string email, string password)
    {
        // Check if the user already exists
        var existingUser = _userRepository.GetUserByEmail(email);

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }
        
        //Create a new user
        var user = new User
        {
            FirstName = name,
            LastName = surname,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);


        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}


public record AuthenticationResult(
    User User,
    string Token
);