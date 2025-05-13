// using MediatR;
using TestClean.Application.Common.Interfaces.Authentication;
using TestClean.Application.Presistence;
using TestClean.Application.Services.Authentication;
using TestClean.Domain.Entities;
using TestClean.Mediator.Interfaces;

namespace TestClean.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    // Your implementation here
    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
         // Check if the user already exists
        var existingUser = _userRepository.GetUserByEmail(command.Email);

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }
        
        //Create a new user
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
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