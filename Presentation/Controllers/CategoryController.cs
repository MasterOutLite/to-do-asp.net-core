using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Categories.Queries.GetCategoryById;
using Application.Categories.Queries.GetCategoryUser;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class CategoryController : ApiController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetCategory([FromQuery] GetCategoryQuery query)
    {
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryById([FromQuery] GetCategoryByIdQuery query)
    {
        var res = await Sender.Send(query);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var res = await Sender.Send(command);
        return CreatedAtAction(nameof(GetCategoryById), new { res }, res);
    }
}