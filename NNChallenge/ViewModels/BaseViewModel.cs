using CommunityToolkit.Mvvm.ComponentModel;

namespace NNChallenge.ViewModels;

public abstract partial class BaseViewModel : ObservableObject, IDisposable
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    private string title = string.Empty;

    public bool IsNotBusy => !IsBusy;

    protected BaseViewModel() { }

    protected void SetBusyState(bool isBusy, string? busyText = null)
    {
        IsBusy = isBusy;
        if (busyText != null)
        {
            Title = busyText;
        }
    }

    public virtual void Dispose() { }
}

public abstract partial class BaseViewModel<T> : BaseViewModel
    where T : class
{
    private T? _parameter;

    protected T? Parameter => _parameter;

    public void SetParameter(T parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        _parameter = parameter;
        Initialize(parameter);
    }

    protected virtual void Initialize(T parameter) { }
}
