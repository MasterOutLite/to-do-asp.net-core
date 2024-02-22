using Application.Auth.Command.Login;
using Application.Auth.Command.RegistrationUser;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class AuthController : ApiController
{
    [HttpPost("registration")]
    [ProducesResponseType(typeof(ResponseToken), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command)
    {
        var token = await Sender.Send(command);
        return Ok(token);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseToken), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var token = await Sender.Send(command);
        return Ok(token);
    }
}