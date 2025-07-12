using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Services;

namespace NNChallenge.Droid.Services;

public static class AndroidServiceCollection
{
    public static IServiceCollection AddAndroidServices(this IServiceCollection services)
    {
        services.AddSingleton<INotificationService, AndroidNotificationService>();
        return services;
    }
}
