using AndroidX.AppCompat.App;
using NNChallenge.Core;
using NNChallenge.Droid.Services;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid.Views;

public abstract class BaseActivity : Activity
{
    protected BaseActivity() { }

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

public abstract class BaseAppCompatActivity : AppCompatActivity
{
    protected BaseAppCompatActivity() { }

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

public abstract class BaseActivity<TViewModel> : BaseActivity
    where TViewModel : BaseViewModel
{
    protected TViewModel ViewModel { get; }

    protected BaseActivity()
    {
        ViewModel = App.GetService<TViewModel>();
    }
}

public abstract class BaseAppCompatActivity<TViewModel> : BaseAppCompatActivity
    where TViewModel : BaseViewModel
{
    protected TViewModel ViewModel { get; }

    protected BaseAppCompatActivity()
    {
        ViewModel = App.GetService<TViewModel>();
    }
}

public abstract class BaseActivity<TViewModel, TParameter> : BaseActivity<TViewModel>
    where TViewModel : BaseViewModel<TParameter>
    where TParameter : class
{
    protected TParameter? Parameter { get; private set; }

    protected BaseActivity()
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

public abstract class BaseAppCompatActivity<TViewModel, TParameter>
    : BaseAppCompatActivity<TViewModel>
    where TViewModel : BaseViewModel<TParameter>
    where TParameter : class
{
    protected TParameter? Parameter { get; private set; }

    protected BaseAppCompatActivity()
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
