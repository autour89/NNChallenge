
namespace NNChallenge.iOS.Views;

public abstract class BaseViewController : UIViewController
{
    protected BaseViewController()
        : base() { }

    protected BaseViewController(string nibName, NSBundle bundle)
        : base(nibName, bundle) { }

    protected BaseViewController(NSCoder coder)
        : base(coder) { }

    protected BaseViewController(IntPtr handle)
        : base(handle) { }

    protected void NavigateToViewController<T>(T viewController, bool animated = true)
        where T : UIViewController
    {
        InvokeOnMainThread(() =>
        {
            PresentViewController(viewController, animated, null);
        });
    }

    protected void PushViewController<T>(T viewController, bool animated = true)
        where T : UIViewController
    {
        InvokeOnMainThread(() =>
        {
            if (NavigationController != null)
            {
                NavigationController.PushViewController(viewController, animated);
            }
            else
            {
                PresentViewController(viewController, animated, null);
            }
        });
    }

    protected void GoBack(bool animated = true)
    {
        InvokeOnMainThread(() =>
        {
            if (NavigationController != null)
            {
                NavigationController.PopViewController(animated);
            }
            else if (PresentingViewController != null)
            {
                DismissViewController(animated, null);
            }
        });
    }
}
