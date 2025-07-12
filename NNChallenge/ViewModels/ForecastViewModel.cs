using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using NNChallenge.Exceptions;
using NNChallenge.Models;
using NNChallenge.Services;

namespace NNChallenge.ViewModels;

public partial class ForecastViewModel : BaseViewModel
{
    private readonly IWeatherService _weatherService;
    private readonly INotificationService? _notificationService;

    [ObservableProperty]
    private string currentLocation = string.Empty;

    [ObservableProperty]
    private WeatherResponse? currentForecast;

    [ObservableProperty]
    private List<ForecastDay> forecastList = [];

    [ObservableProperty]
    private bool hasForecast;

    [ObservableProperty]
    private string temperatureDisplay = string.Empty;

    [ObservableProperty]
    private string conditionDisplay = string.Empty;

    public ForecastViewModel(
        IWeatherService weatherService,
        INotificationService? notificationService = null
    )
    {
        _weatherService = weatherService;
        _notificationService = notificationService;
        Title = "Weather Forecast";
    }

    [RelayCommand]
    private async Task LoadForecastAsync(string location)
    {
        if (string.IsNullOrEmpty(location))
            return;

        SetBusyState(true, "Loading forecast...");

        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new NetworkException(
                    "No internet connection available. Please check your network settings and try again."
                );
            }

            CurrentLocation = location;

            var weatherResponse = await _weatherService.GetForecastAsync(location, 3);
            CurrentForecast = weatherResponse;
            ForecastList = weatherResponse.Forecast.ForecastDay;

            if (weatherResponse.Current != null)
            {
                TemperatureDisplay =
                    $"{weatherResponse.Current.TempC:F1}°C / {weatherResponse.Current.TempF:F1}°F";
                ConditionDisplay = weatherResponse.Current.Condition.Text;
            }

            HasForecast = weatherResponse != null && weatherResponse.Forecast.ForecastDay.Any();

            if (_notificationService != null)
            {
                await _notificationService.ShowDialogAsync(
                    "Weather Loaded",
                    $"Forecast for {location} loaded successfully"
                );
            }
        }
        catch (NetworkException ex)
        {
            if (_notificationService != null)
            {
                await _notificationService.ShowDialogAsync("Network Error", ex.Message);
            }

            HasForecast = false;
            TemperatureDisplay = "N/A";
            ConditionDisplay = "No connection";
        }
        catch (Exception)
        {
            if (_notificationService != null)
            {
                await _notificationService.ShowDialogAsync(
                    "Error",
                    "Unable to load weather data. Please try again."
                );
            }

            HasForecast = false;
            TemperatureDisplay = "N/A";
            ConditionDisplay = "Unable to load weather data";
        }
        finally
        {
            SetBusyState(false);
        }
    }

    [RelayCommand]
    private async Task RefreshForecastAsync()
    {
        if (!string.IsNullOrEmpty(CurrentLocation))
        {
            await LoadForecastAsync(CurrentLocation);
        }
    }

    [RelayCommand]
    private void ClearForecast()
    {
        CurrentForecast = null;
        ForecastList.Clear();
        HasForecast = false;
        CurrentLocation = string.Empty;
        TemperatureDisplay = string.Empty;
        ConditionDisplay = string.Empty;
    }

    [RelayCommand]
    private async Task TestWeatherApiAsync()
    {
        await LoadForecastAsync("Berlin");
    }

    public async Task LoadForecastForSelectedLocationAsync()
    {
        if (!string.IsNullOrEmpty(CurrentLocation))
        {
            await LoadForecastAsync(CurrentLocation);
        }
    }
}
