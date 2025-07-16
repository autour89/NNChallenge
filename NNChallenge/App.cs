using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NNChallenge.Configuration;
using NNChallenge.Mappers;
using NNChallenge.Services;
using NNChallenge.Services.Http;
using NNChallenge.ViewModels;

namespace NNChallenge.Core;

public class App
{
    static IServiceProvider? _container;
    static IServiceCollection? _services;

    public static void Initialize()
    {
        _services = new ServiceCollection();
        RegisterCoreServices();
        _container = _services.BuildServiceProvider();
    }

    static void RegisterCoreServices()
    {
        if (_services == null)
            return;

        MapsterConfig.ConfigureMappings();

        _services.AddMapster();

        _services.AddSingleton(new WeatherApiSettings());

        _services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        _services
            .AddHttpClient($"{nameof(ApiClient<IWeatherApi>)}")
            .AddHttpMessageHandler<ApiMessageHandler>();

        _services.AddScoped(typeof(IApiClient<>), typeof(ApiClient<>));

        _services.AddTransient<IWeatherService, WeatherService>();

        _services.AddTransient<LocationViewModel>();
        _services.AddTransient<ForecastViewModel>();
    }

    public static T GetService<T>()
        where T : notnull
    {
        if (_container is null)
        {
            throw new InvalidOperationException("App not initialized");
        }
        return _container.GetRequiredService<T>();
    }
}
