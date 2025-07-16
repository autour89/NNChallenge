using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Networking;
using NNChallenge.Constants;
using NNChallenge.Exceptions;
using NNChallenge.Models.DAO;
using NNChallenge.Services;

namespace NNChallenge.ViewModels;

public partial class LocationViewModel : BaseViewModel
{
    private readonly IWeatherService _weatherService;
    readonly ILogger<LocationViewModel> _logger;

    [ObservableProperty]
    private string selectedLocation = string.Empty;

    [ObservableProperty]
    private string[] availableLocations = LocationConstants.LOCATIONS;

    [ObservableProperty]
    private bool isLocationSelected;

    [ObservableProperty]
    private WeatherDataDAO? weatherData;

    public Action<WeatherDataDAO>? OnWeatherDataLoaded { get; set; }

    public LocationViewModel(IWeatherService weatherService, ILogger<LocationViewModel> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
        Title = "Select Location";
    }

    [RelayCommand]
    private async Task SelectLocationAsync(string location)
    {
        if (string.IsNullOrEmpty(location))
        {
            return;
        }

        _logger.LogInformation("Starting weather data load for location: {Location}", location);
        SetBusyState(true, "Loading weather data...");

        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new NetworkException(
                    "No internet connection available. Please check your network settings and try again."
                );
            }

            SelectedLocation = location;

            var weatherResponse = await _weatherService.GetForecastAsync(location, 3);
            WeatherData = weatherResponse.Adapt<WeatherDataDAO>();

            if (WeatherData != null)
            {
                OnWeatherDataLoaded?.Invoke(WeatherData);
            }
        }
        catch (NetworkException ex)
        {
            _logger.LogError(ex, "Network error occurred while loading weather data");
            WeatherData = null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while loading weather data");
            WeatherData = null;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
