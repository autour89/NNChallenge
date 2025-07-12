using NNChallenge.Models;
using Refit;

namespace NNChallenge.Services.Http;

public interface IWeatherApi
{
    [Get("/v1/forecast.json")]
    Task<WeatherResponse> GetForecastAsync(
        [Query] string key,
        [Query] string q,
        [Query] int days = 3,
        [Query] string aqi = "no",
        [Query] string alerts = "no");
}
