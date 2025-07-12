namespace NNChallenge.Services;

public interface INotificationService
{
    Task ShowDialogAsync(string title, string message);
}
