using Application.ToDos.Queries.GetToDoById;
using Application.Weathers.Queries.GetWeather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class WeatherForecastController : ApiController
{
    [HttpGet(Name = "GetWeatherForecast")]
    [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetWeatherCommand query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }
}