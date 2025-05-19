
using Namespace.Application.Authentication.Queries.Login;
using TestClean.Application.Common.Interfaces.Authentication;
using TestClean.Application.Presistence;
using TestClean.Application.Services.Authentication;
using TestClean.Mediator.Interfaces;

namespace TestClean.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(query.Email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Check if the password is correct
        if (user.Password != query.Password)
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