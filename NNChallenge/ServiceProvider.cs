using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Services;

namespace NNChallenge;

public static class ServiceProvider
{
    private static IServiceProvider? _serviceProvider;

    public static IServiceProvider I
    {
        get
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException(
                    "ServiceProvider has not been initialized. Call Initialize() first."
                );
            }
            return _serviceProvider;
        }
    }

    public static void Initialize(Action<IServiceCollection>? configurePlatformServices = null)
    {
        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

        services.AddViewModels();

        configurePlatformServices?.Invoke(services);

        _serviceProvider = services.BuildServiceProvider();
    }

    public static T GetService<T>()
        where T : notnull
    {
        return I.GetRequiredService<T>();
    }

    public static void Reset()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
        _serviceProvider = null;
    }
}
