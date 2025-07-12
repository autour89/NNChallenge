using NNChallenge.Models;

namespace NNChallenge.Services;

public interface IWeatherService
{
    Task<WeatherResponse> GetForecastAsync(string location, int days = 3);
}
