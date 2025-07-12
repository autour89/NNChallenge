using NNChallenge.Configuration;
using NNChallenge.Exceptions;
using NNChallenge.Models;
using NNChallenge.Services.Http;

namespace NNChallenge.Services;

public class WeatherService : IWeatherService
{
    private readonly IApiClient<IWeatherApi> _apiClient;
    private readonly WeatherApiSettings _settings;

    public WeatherService(
        IApiClient<IWeatherApi> apiClient, 
        WeatherApiSettings settings)
    {
        _apiClient = apiClient;
        _settings = settings;
    }

    public async Task<WeatherResponse> GetForecastAsync(string location, int days = 3)
    {
        try
        {
            var client = _apiClient.GetClient(_settings.BaseUrl);
            var response = await client.GetForecastAsync(_settings.ApiKey, location, days);
            return response;
        }
        catch (NetworkException)
        {
            // Re-throw network exceptions to be handled by ViewModels
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
