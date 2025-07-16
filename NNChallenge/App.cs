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
    private static bool _isInitialized = false;

    public static void Initialize(Action<IServiceCollection>? platformServices = null)
    {
        if (_isInitialized)
        {
            return;
        }

        _services = new ServiceCollection();
        RegisterCoreServices(_services);
        platformServices?.Invoke(_services);

        _container = _services.BuildServiceProvider();
        _isInitialized = true;
    }

    static void RegisterCoreServices(IServiceCollection services)
    {
        if (services == null)
            return;

        MapsterConfig.ConfigureMappings();

        services.AddMapster();

        services.AddSingleton(new WeatherApiSettings());

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        services
            .AddHttpClient($"{nameof(ApiClient<IWeatherApi>)}")
            .AddHttpMessageHandler<ApiMessageHandler>();

        services.AddScoped(typeof(IApiClient<>), typeof(ApiClient<>));

        services.AddTransient<IWeatherService, WeatherService>();

        services.AddTransient<LocationViewModel>();
        services.AddTransient<ForecastViewModel>();
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
