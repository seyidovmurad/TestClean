namespace TestClean.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public IActionResult GetError()
    {
        return Problem("An error occurred.");
    }
}