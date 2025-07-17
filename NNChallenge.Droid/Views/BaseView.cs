using AndroidX.AppCompat.App;
using NNChallenge.Core;
using NNChallenge.Droid.Services;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid.Views;

public abstract class BaseView : Activity
{
    protected BaseView() { }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        NavigationService.CurrentActivity = this;
        InitializeView(savedInstanceState);
    }

    protected override void OnResume()
    {
        base.OnResume();
        NavigationService.CurrentActivity = this;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (NavigationService.CurrentActivity == this)
        {
            NavigationService.CurrentActivity = null;
        }
    }

    protected abstract void InitializeView(Bundle? savedInstanceState);
}

public abstract class BaseAppCompatView : AppCompatActivity
{
    protected BaseAppCompatView() { }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        NavigationService.CurrentActivity = this;
        InitializeView(savedInstanceState);
    }

    protected override void OnResume()
    {
        base.OnResume();
        NavigationService.CurrentActivity = this;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (NavigationService.CurrentActivity == this)
        {
            NavigationService.CurrentActivity = null;
        }
    }

    protected abstract void InitializeView(Bundle? savedInstanceState);
}

public abstract class BaseView<TViewModel> : BaseView
    where TViewModel : BaseViewModel
{
    protected TViewModel ViewModel { get; }

    protected BaseView()
    {
        ViewModel = App.GetService<TViewModel>();
    }
}

public abstract class BaseAppCompatView<TViewModel> : BaseAppCompatView
    where TViewModel : BaseViewModel
{
    protected TViewModel ViewModel { get; }

    protected BaseAppCompatView()
    {
        ViewModel = App.GetService<TViewModel>();
    }
}

public abstract class BaseView<TViewModel, TParameter> : BaseView<TViewModel>
    where TViewModel : BaseViewModel<TParameter>
    where TParameter : class
{
    protected TParameter? Parameter { get; private set; }

    protected BaseView()
        : base() { }

    protected void SetParameter(TParameter parameter)
    {
        if (parameter != null)
        {
            Parameter = parameter;
            ViewModel.SetParameter(parameter);
        }
    }
}

public abstract class BaseAppCompatView<TViewModel, TParameter> : BaseAppCompatView<TViewModel>
    where TViewModel : BaseViewModel<TParameter>
    where TParameter : class
{
    protected TParameter? Parameter { get; private set; }

    protected BaseAppCompatView()
        : base() { }

    protected void SetParameter(TParameter parameter)
    {
        if (parameter != null)
        {
            Parameter = parameter;
            ViewModel.SetParameter(parameter);
        }
    }
}
