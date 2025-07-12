using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Services;

namespace NNChallenge.iOS.Services;

public static class IOSServiceCollection
{
    public static IServiceCollection AddIOSServices(this IServiceCollection services)
    {
        services.AddSingleton<INotificationService, IOSNotificationService>();
        return services;
    }
}
