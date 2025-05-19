using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Namespace.Application.Authentication.Queries.Login;
using TestClean.Application.Authentication.Commands.Register;
using TestClean.Contracts.Authentication;

namespace TestClean.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly TestClean.Mediator.Interfaces.ISender _sender;

    public AuthenticationController(Mediator.Interfaces.ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var response = await _sender.Send(command);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password
        );

        var response = await _sender.Send(query);

        return Ok(response);
    }
}
