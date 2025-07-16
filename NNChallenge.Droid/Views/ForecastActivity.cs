using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Core;
using NNChallenge.Models;
using NNChallenge.ViewModels;
using System.Text.Json;

namespace NNChallenge.Droid.Views;

[Activity(Label = "Forecast")]
public class ForecastActivity : Activity
{
    private ForecastViewModel? _forecastViewModel;
    private RecyclerView _recyclerView = null!;
    private HourlyForecastAdapter _adapter = null!;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.forecast_activity);

        _forecastViewModel = App.GetService<ForecastViewModel>();

        SetupRecyclerView();
        LoadWeatherData();
    }

    private void SetupRecyclerView()
    {
        _recyclerView = FindViewById<RecyclerView>(Resource.Id.hourly_forecast_recycler)!;
        _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
        
        _adapter = new HourlyForecastAdapter(new List<HourlyForecastItem>());
        _recyclerView.SetAdapter(_adapter);
    }

    private void LoadWeatherData()
    {
        try
        {
            var weatherDataJson = Intent?.GetStringExtra("WeatherData");
            if (!string.IsNullOrEmpty(weatherDataJson))
            {
                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(weatherDataJson);
                if (weatherData != null)
                {
                    // Set the title to show the city name
                    if (!string.IsNullOrEmpty(weatherData.Location?.Name))
                    {
                        Title = $"{weatherData.Location.Name} Forecast";
                    }
                    else
                    {
                        Title = "3-Day Hourly Forecast";
                    }

                    var hourlyItems = GenerateHourlyForecastItems(weatherData);
                    _adapter.UpdateData(hourlyItems);
                }
            }
        }
        catch (Exception ex)
        {
            Android.Util.Log.Error("ForecastActivity", $"Error loading weather data: {ex.Message}");
        }
    }

    private List<HourlyForecastItem> GenerateHourlyForecastItems(WeatherResponse weatherData)
    {
        var hourlyItems = new List<HourlyForecastItem>();

        if (weatherData.Forecast?.ForecastDay != null)
        {
            foreach (var day in weatherData.Forecast.ForecastDay)
            {
                if (day.Hour != null)
                {
                    foreach (var hour in day.Hour)
                    {
                        hourlyItems.Add(HourlyForecastItem.FromHour(hour, day.Date));
                    }
                }
            }
        }

        return hourlyItems;
    }
}
