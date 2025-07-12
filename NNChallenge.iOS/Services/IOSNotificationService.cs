using Foundation;
using NNChallenge.Services;
using UIKit;

namespace NNChallenge.iOS.Services;

/// <summary>
/// iOS implementation of notification service
/// </summary>
public class IOSNotificationService : INotificationService
{
    public async Task ShowDialogAsync(string title, string message)
    {
        try
        {
            await InvokeOnMainThreadAsync(() =>
            {
                var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                var window = UIApplication.SharedApplication.KeyWindow;
                var rootViewController = window?.RootViewController;
                
                if (rootViewController != null)
                {
                    rootViewController.PresentViewController(alertController, true, null);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Cannot show dialog - no root view controller available");
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error showing dialog: {ex.Message}");
        }
    }

    private static Task InvokeOnMainThreadAsync(Action action)
    {
        var tcs = new TaskCompletionSource<bool>();
        
        NSOperationQueue.MainQueue.AddOperation(() =>
        {
            try
            {
                action();
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });
        
        return tcs.Task;
    }
}