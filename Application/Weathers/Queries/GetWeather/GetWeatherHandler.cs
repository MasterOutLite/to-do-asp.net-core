using Application.Abstractions.Messaging;

namespace Application.Weathers.Queries.GetWeather;

public record GetWeatherCommand() : IQuery<List<WeatherForecast>>;

public class GetWeatherHandler : IQueryHandler<GetWeatherCommand, List<WeatherForecast>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<List<WeatherForecast>> Handle(GetWeatherCommand request, CancellationToken cancellationToken)
    {
        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        return Task.FromResult(forecast);
    }
}