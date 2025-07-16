using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapster;
using Microsoft.Maui.Networking;
using NNChallenge.Exceptions;
using NNChallenge.Models.DAO;
using NNChallenge.Services;

namespace NNChallenge.ViewModels;

public partial class ForecastViewModel : BaseViewModel
{
    private readonly IWeatherService _weatherService;

    [ObservableProperty]
    private List<HourlyForecastItem> hourlyItems = [];

    public ForecastViewModel(IWeatherService weatherService)
    {
        _weatherService = weatherService;
        Title = "Weather Forecast";
    }

    [RelayCommand]
    private async Task LoadForecastAsync(string location)
    {
        if (string.IsNullOrEmpty(location))
        {
            return;
        }

        SetBusyState(true, "Loading forecast...");

        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new NetworkException(
                    "No internet connection available. Please check your network settings and try again."
                );
            }

            var weatherResponse = await _weatherService.GetForecastAsync(location, 3);
            var weatherDataDAO = weatherResponse.Adapt<WeatherDataDAO>();

            SetWeatherData(weatherDataDAO);
        }
        catch (NetworkException) { }
        catch (Exception) { }
        finally
        {
            IsBusy = false;
        }
    }

    public void SetWeatherData(WeatherDataDAO weatherData)
    {
        if (weatherData is null)
        {
            return;
        }

        HourlyItems.Clear();

        if (weatherData.Forecast?.ForecastDays != null)
        {
            foreach (var forecastDay in weatherData.Forecast.ForecastDays)
            {
                if (forecastDay.Hours != null)
                {
                    foreach (var hour in forecastDay.Hours)
                    {
                        HourlyItems.Add(
                            new HourlyForecastItem
                            {
                                Time = hour.Time,
                                TempC = hour.TempC,
                                TempF = hour.TempF,
                                ConditionText = hour.Condition.Text,
                                Humidity = hour.Humidity,
                                IsDay = hour.IsDay,
                            }
                        );
                    }
                }
            }
        }
    }
}
