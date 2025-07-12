using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNChallenge.Constants;
using NNChallenge.Services;

namespace NNChallenge.ViewModels;

public partial class LocationViewModel : BaseViewModel
{
    private readonly INotificationService? _notificationService;

    [ObservableProperty]
    private string selectedLocation = string.Empty;

    [ObservableProperty]
    private string[] availableLocations = LocationConstants.LOCATIONS;

    [ObservableProperty]
    private bool isLocationSelected;

    public LocationViewModel(INotificationService? notificationService = null)
    {
        _notificationService = notificationService;
        Title = "Select Location";
        UpdateLocationSelection();
    }

    [RelayCommand]
    private async Task SelectLocationAsync(string location)
    {
        if (string.IsNullOrEmpty(location))
            return;

        SetBusyState(true, "Selecting location...");

        try
        {
            SelectedLocation = location;
            UpdateLocationSelection();

            if (_notificationService != null)
            {
                await _notificationService.ShowDialogAsync(
                    "Location Selected",
                    $"Selected {location}"
                );
            }

            await OnLocationSelectedAsync(location);
        }
        catch (Exception ex)
        {
            if (_notificationService != null)
            {
                await _notificationService.ShowDialogAsync(
                    "Selection Error",
                    $"Error selecting location: {ex.Message}"
                );
            }

            Debug.WriteLine($"Error selecting location: {ex.Message}");
        }
        finally
        {
            SetBusyState(false);
        }
    }

    [RelayCommand]
    private void SetSelectedLocation(string location)
    {
        if (string.IsNullOrEmpty(location))
            return;

        SelectedLocation = location;
        UpdateLocationSelection();
    }

    [RelayCommand]
    private void ClearSelection()
    {
        SelectedLocation = string.Empty;
        UpdateLocationSelection();
    }

    private void UpdateLocationSelection()
    {
        IsLocationSelected = !string.IsNullOrEmpty(SelectedLocation);
    }

    protected virtual Task OnLocationSelectedAsync(string location)
    {
        return Task.CompletedTask;
    }
}
