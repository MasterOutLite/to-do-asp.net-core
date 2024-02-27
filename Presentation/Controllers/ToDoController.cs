using System.Security.Claims;
using Application.Common.Models;
using Application.ToDos.Commands.CreateToDo;
using Application.ToDos.Commands.DeleteToDo;
using Application.ToDos.Commands.UpdateToDo;
using Application.ToDos.Queries.GetToDoById;
using Application.ToDos.Queries.GetToDoList;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
using Serilog;

namespace Presentation.Controllers;

public class ToDoController : ApiController
{
    [Authorize]
    [HttpGet("all")]
    [ProducesResponseType(typeof(PaginationResponse<ToDoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetToDos([FromQuery] GetToDoListRequest request)
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);

        var query = new GetToDoListQuery(request.Page,
            request.Count == 0 ? 10 : request.Count,
            userId,
            request.SortOrder,
            request.CategoryId,
            request.SortColumn);
        var res = await Sender.Send(query);
        Log.Information("Response {@Res}, Length {Length}", res, res.Total);
        return Ok(res);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ToDoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetToDoById([FromQuery] GetToDoByIdRequest request)
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);
        var query = new GetToDoByIdQuery(request.Id, userId);
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ToDoResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateToDo(
        [FromBody] CreateToDoRequest request,
        CancellationToken cancellationToken
    )
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);

        var command = new CreateToDoCommand(
            request.Title,
            request.Description,
            request.Done,
            request.CategoryId,
            userId
        );

        var res = await Sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetToDoById), new { res.Id }, res);
    }

    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromQuery] DeleteToDoRequest request)
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);
        var command = new DeleteToDoCommand(request.Id, userId);
        var res = await Sender.Send(command);
        if (!res)
        {
            return NotFound(request);
        }

        return NoContent();
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromQuery] long id, [FromBody] UpdateToDoRequest request)
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);
        var command = new UpdateToDoCommand(request.Title,
            request.Description,
            request.Done,
            request.CategoryId,
            id, userId);

        var res = await Sender.Send(command);

        if (!res)
        {
            return NotFound(request);
        }

        return NoContent();
    }
}