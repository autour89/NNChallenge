using Android.Content;
using Android.Runtime;
using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Core;
using NNChallenge.Droid.Services;
using NNChallenge.Services;

namespace NNChallenge.Droid;

[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership transfer)
    : Application(handle, transfer)
{
    public override void OnCreate()
    {
        base.OnCreate();

        App.Initialize(services =>
        {
            services.AddSingleton<Context>(this);
            services.AddSingleton<INavigationService, NavigationService>();
        });
    }
}
