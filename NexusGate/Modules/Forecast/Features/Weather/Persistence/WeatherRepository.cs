using NexusGate.Modules.Forecast.Domain;

namespace NexusGate.Modules.Forecast.Features.Weather.Persistence;

public interface IWeatherRepository
{
    public Task<List<WeatherForecast>> GetForecastAsync(int count, CancellationToken cancellationToken = default);
    public Task<WeatherForecast> GetSingleForecastAsync(CancellationToken cancellationToken = default);
}

public class WeatherRepository : IWeatherRepository
{
    private readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public async Task<List<WeatherForecast>> GetForecastAsync(int count, CancellationToken cancellationToken = default)
    {
        var forecasts = Enumerable.Range(1, count).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)]
                ))
            .ToList();

        return await Task.FromResult(forecasts);
    }

    public async Task<WeatherForecast> GetSingleForecastAsync(CancellationToken cancellationToken = default)
    {
        var forecast = new WeatherForecast
        (
            Date: DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(31))),
            TemperatureC: Random.Shared.Next(-20, 55),
            Summary: _summaries[Random.Shared.Next(_summaries.Length)]
        );

        return await Task.FromResult(forecast);
    }
}