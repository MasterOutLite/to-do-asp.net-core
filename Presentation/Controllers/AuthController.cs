using Application.Auth.Command.Login;
using Application.Auth.Command.RegistrationUser;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class AuthController : ApiController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command)
    {
        var token = await Sender.Send(command);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var token = await Sender.Send(command);
        return Ok(token);
    }
}