using Android.Content;
using Android.Runtime;
using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Core;
using NNChallenge.Droid.Services;
using NNChallenge.Services;

namespace NNChallenge.Droid;

[Application]
public class WeatherApplication(IntPtr handle, JniHandleOwnership ownership)
    : Application(handle, ownership)
{
    public static Context? CurrentContext { get; private set; }

    public override void OnCreate()
    {
        base.OnCreate();

        CurrentContext = this;

        App.Initialize(services =>
        {
            services.AddSingleton<INavigationService, NavigationService>();
        });
    }
}
