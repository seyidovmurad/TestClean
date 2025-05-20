using System.Threading.Tasks;
using MapsterMapper;
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
    private readonly IMapper _mapper;

    public AuthenticationController(Mediator.Interfaces.ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var response = await _sender.Send(command);

        return Ok(_mapper.Map<AuthenticationResponse>(response));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var response = await _sender.Send(query);

        return Ok(_mapper.Map<AuthenticationResponse>(response));
    }
}
