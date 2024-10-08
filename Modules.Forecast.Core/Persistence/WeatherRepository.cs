using Modules.Forecast.Core.Domain;

namespace Modules.Forecast.Core.Persistence;

public interface IWeatherRepository
{
    public Task<List<WeatherForecast>> GetForecastAsync(CancellationToken cancellationToken = default);
}

public class WeatherRepository : IWeatherRepository
{
    public async Task<List<WeatherForecast>> GetForecastAsync(CancellationToken cancellationToken = default)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 10).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToList();

        return await Task.FromResult(forecast);
    }
}