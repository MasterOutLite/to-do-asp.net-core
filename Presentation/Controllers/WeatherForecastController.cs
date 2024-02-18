using Application.ToDos.Queries.GetToDoById;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class WeatherForecastController : ApiController
{
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get([FromQuery] GetToDoByIdQuery query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }
}