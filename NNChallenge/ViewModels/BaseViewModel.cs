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
