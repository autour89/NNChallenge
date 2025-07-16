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

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        InitializeView();

        if (NavigationController is not null)
        {
            AppDelegate.NavigationController = NavigationController;
        }
    }

    protected abstract void InitializeView();
}
