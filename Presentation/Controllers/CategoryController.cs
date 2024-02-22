using System.Security.Claims;
using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Queries.GetCategory;
using Application.Categories.Queries.GetCategoryById;
using Application.Common.Models;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class CategoryController : ApiController
{
    [Authorize]
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategory()
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);
        var query = new GetCategoryQuery(userId);
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById([FromQuery] GetCategoryByIdRequest request)
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);
        var query = new GetCategoryByIdQuery(request.Id, userId);
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);

        var command = new CreateCategoryCommand(request.Name, request.Description, userId);
        var res = await Sender.Send(command);
        return CreatedAtAction(nameof(GetCategoryById), new { res.Id }, res);
    }

    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory([FromQuery] DeleteCategoryRequest request)
    {
        var userId = long.Parse(User.FindFirstValue(JwtClaims.Id)!);

        var command = new DeleteCategoryCommand(request.Id, userId);
        var res = await Sender.Send(command);
        if (!res)
        {
            return NotFound(request);
        }

        return NoContent();
    }
}