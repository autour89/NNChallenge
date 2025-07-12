using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Configuration;
using NNChallenge.Services.Http;
using NNChallenge.ViewModels;

namespace NNChallenge.Services;

public static class ServiceCollection
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton(new WeatherApiSettings());

        services
            .AddHttpClient($"{nameof(ApiClient<IWeatherApi>)}")
            .AddHttpMessageHandler<ApiMessageHandler>();

        services.AddScoped(typeof(IApiClient<>), typeof(ApiClient<>));

        services.AddTransient<IWeatherService, WeatherService>();

        services.AddTransient<LocationViewModel>();
        services.AddTransient<ForecastViewModel>();

        return services;
    }
}
