using System.Net;
using Application.Abstractions.Models;
using Application.Categories.Commands.CreateCategory;
using Application.Categories.Queries.GetCategoryById;
using Application.Categories.Queries.GetCategoryUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class CategoryController : ApiController
{
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategory([FromQuery] GetCategoryQuery query)
    {
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [HttpGet]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById([FromQuery] GetCategoryByIdQuery query)
    {
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var res = await Sender.Send(command);
        return CreatedAtAction(nameof(GetCategoryById), new { res }, res);
    }
}