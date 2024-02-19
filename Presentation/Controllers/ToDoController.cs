﻿using Application.Common.Models;
using Application.ToDos.Commands.CreateToDo;
using Application.ToDos.Queries.GetToDoById;
using Application.ToDos.Queries.GetToDoList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
using Serilog;

namespace Presentation.Controllers;

public class ToDoController : ApiController
{
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<ToDoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetToDos([FromQuery] GetToDoListQuery query)
    {
        var res = await Sender.Send(query);
        Log.Information("Response {@Res}, Length {Length}", res, res.Capacity);
        return Ok(res);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ToDoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetToDoById([FromQuery] GetToDoByIdQuery query)
    {
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateToDo([FromBody] CreateToDoCommand command,
        CancellationToken cancellationToken)
    {
        var res = await Sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetToDoById), new { res }, res);
    }
}