using NNChallenge.Core;
using NNChallenge.iOS.Views;

namespace NNChallenge.iOS;

[Register("AppDelegate")]
public class AppDelegate : UIResponder, IUIApplicationDelegate
{
    [Export("window")]
    public UIWindow? Window { get; set; }

    public static UINavigationController? NavigationController { get; private set; }

    [Export("application:didFinishLaunchingWithOptions:")]
    public bool FinishedLaunching(UIApplication application, NSDictionary? launchOptions)
    {
        RegisterServices();
        NavigateToRoot();
        return true;
    }

    [Export("application:configurationForConnectingSceneSession:options:")]
    public UISceneConfiguration GetConfiguration(
        UIApplication application,
        UISceneSession connectingSceneSession,
        UISceneConnectionOptions options
    )
    {
        // Called when a new scene session is being created.
        // Use this method to select a configuration to create the new scene with.
        return UISceneConfiguration.Create("Default Configuration", connectingSceneSession.Role);
    }

    [Export("application:didDiscardSceneSessions:")]
    public void DidDiscardSceneSessions(
        UIApplication application,
        NSSet<UISceneSession> sceneSessions
    )
    {
        // Called when the user discards a scene session.
        // If any sessions were discarded while the application was not running, this will be called shortly after `FinishedLaunching`.
        // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
    }

    void NavigateToRoot()
    {
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        var firstView = new LocationViewController();
        UINavigationController navigationController = new(firstView);
        Window.RootViewController = navigationController;
        NavigationController = navigationController;
        Window.MakeKeyAndVisible();
    }

    private static void RegisterServices()
    {
        App.Initialize();
    }
}
