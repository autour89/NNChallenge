using NNChallenge.Configuration;
using NNChallenge.Models;
using NNChallenge.Services.Http;

namespace NNChallenge.Services;

public class WeatherService(IApiClient<IWeatherApi> apiClient, WeatherApiSettings settings)
    : IWeatherService
{
    private readonly IApiClient<IWeatherApi> _apiClient = apiClient;
    private readonly WeatherApiSettings _settings = settings;

    public async Task<WeatherResponse> GetForecastAsync(string location, int days = 3)
    {
        var client = _apiClient.GetClient(_settings.BaseUrl);

        return await client.GetForecastAsync(_settings.ApiKey, location, days);
    }
}
