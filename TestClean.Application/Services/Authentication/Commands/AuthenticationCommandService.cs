using TestClean.Application.Common.Interfaces.Authentication;
using TestClean.Application.Presistence;
using TestClean.Domain.Entities;

namespace TestClean.Application.Services.Authentication.Commands;
public interface IAuthenticationCommandService
{
    AuthenticationResult Register(string name, string surname, string email, string password);

}


public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
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

