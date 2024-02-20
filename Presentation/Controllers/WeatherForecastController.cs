using Application.Weathers.Queries.GetWeather;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

public class WeatherForecastController : ApiController
{
    //[HasPermission(Permissions.ReadUser)]
    [Authorize(policy: "User")]
    [HttpGet]
    [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetWeatherCommand query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }
}