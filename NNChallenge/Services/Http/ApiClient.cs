using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;

namespace NNChallenge.Services.Http;

public interface IApiClient<T>
{
    T GetClient(string url, string? token = null);
}

public class ApiClient<T> : IApiClient<T>
{
    private readonly TimeSpan _timeout = TimeSpan.FromSeconds(60);
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public T GetClient(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient("WeatherApiClient");
        client.BaseAddress = new Uri(url);
        client.Timeout = _timeout;

        if (!string.IsNullOrWhiteSpace(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }

        return RestService.For<T>(client, ApiClient<T>.WithSettings());
    }

    private static RefitSettings WithSettings() =>
        new()
        {
            ContentSerializer = new SystemTextJsonContentSerializer(
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true,
                }
            ),
        };
}
