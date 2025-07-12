using Android.App;
using AndroidX.AppCompat.App;
using NNChallenge.Services;

namespace NNChallenge.Droid.Services;

/// <summary>
/// Android implementation of notification service
/// </summary>
public class AndroidNotificationService : INotificationService
{
    public async Task ShowDialogAsync(string title, string message)
    {
        try
        {
            // Get the current activity
            var activity = GetCurrentActivity();
            if (activity == null)
            {
                System.Diagnostics.Debug.WriteLine($"Cannot show dialog - no activity context available");
                return;
            }

            await Task.Run(() =>
            {
                activity.RunOnUiThread(() =>
                {
                    try
                    {
                        var dialog = new AndroidX.AppCompat.App.AlertDialog.Builder(activity)
                            .SetTitle(title)
                            .SetMessage(message)
                            .SetPositiveButton("OK", (s, e) => { })
                            .Create();

                        dialog?.Show();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error creating dialog: {ex.Message}");
                    }
                });
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error showing dialog: {ex.Message}");
        }
    }

    private static Activity? GetCurrentActivity()
    {
        try
        {
            // Try to get the current activity from the application context
            var app = Android.App.Application.Context as Android.App.Application;
            if (app != null)
            {
                // This is a simplified approach - in a real app you'd maintain a reference to the current activity
                // For now, we'll return null and log a message
                System.Diagnostics.Debug.WriteLine("Current activity retrieval not implemented - would need activity reference");
            }
            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting current activity: {ex.Message}");
            return null;
        }
    }
}