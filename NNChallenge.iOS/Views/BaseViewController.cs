using NNChallenge.Core;
using NNChallenge.ViewModels;

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

public abstract class BaseViewController<TViewModel> : BaseViewController
    where TViewModel : BaseViewModel
{
    protected TViewModel ViewModel { get; }

    protected BaseViewController()
        : base()
    {
        ViewModel = App.GetService<TViewModel>();
    }

    protected BaseViewController(string nibName, NSBundle bundle)
        : base(nibName, bundle)
    {
        ViewModel = App.GetService<TViewModel>();
    }

    protected BaseViewController(NSCoder coder)
        : base(coder)
    {
        ViewModel = App.GetService<TViewModel>();
    }

    protected BaseViewController(IntPtr handle)
        : base(handle)
    {
        ViewModel = App.GetService<TViewModel>();
    }
}

public abstract class BaseViewController<TViewModel, TParameter> : BaseViewController<TViewModel>
    where TViewModel : BaseViewModel<TParameter>
    where TParameter : class
{
    protected TParameter? Parameter { get; }

    protected BaseViewController(TParameter parameter)
        : base()
    {
        Parameter = parameter;
        InitializeViewModel(parameter);
    }

    protected BaseViewController(string nibName, NSBundle bundle, TParameter parameter)
        : base(nibName, bundle)
    {
        Parameter = parameter;
        InitializeViewModel(parameter);
    }

    protected BaseViewController(NSCoder coder, TParameter parameter)
        : base(coder)
    {
        Parameter = parameter;
        InitializeViewModel(parameter);
    }

    protected BaseViewController(IntPtr handle, TParameter parameter)
        : base(handle)
    {
        Parameter = parameter;
        InitializeViewModel(parameter);
    }

    private void InitializeViewModel(TParameter parameter)
    {
        if (parameter != null)
        {
            ViewModel.SetParameter(parameter);
        }
    }
}
