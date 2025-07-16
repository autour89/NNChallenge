using Android.Runtime;
using NNChallenge.Core;

namespace NNChallenge.Droid;

/// <summary>
/// Application class for Android - this is where we initialize the DI container
/// </summary>
[Application]
public class MainApplication : Application
{
    public MainApplication(IntPtr handle, JniHandleOwnership transfer)
        : base(handle, transfer) { }

    public override void OnCreate()
    {
        base.OnCreate();

        App.Initialize();
    }
}
