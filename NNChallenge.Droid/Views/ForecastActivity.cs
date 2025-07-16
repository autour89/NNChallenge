using System.Text.Json;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Core;
using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;

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

        _adapter = new HourlyForecastAdapter([]);
        _recyclerView.SetAdapter(_adapter);
    }

    private void LoadWeatherData()
    {
        try
        {
            var weatherDataJson = Intent?.GetStringExtra("WeatherData");
            if (!string.IsNullOrEmpty(weatherDataJson))
            {
                var weatherData = JsonSerializer.Deserialize<WeatherDataDAO>(weatherDataJson);
                if (weatherData != null)
                {
                    if (!string.IsNullOrEmpty(weatherData.Location?.Name))
                    {
                        Title = $"{weatherData.Location.Name} Forecast";
                    }
                    else
                    {
                        Title = "3-Day Hourly Forecast";
                    }

                    _forecastViewModel?.SetWeatherData(weatherData);

                    if (_forecastViewModel?.HourlyItems != null)
                    {
                        _adapter.UpdateData(_forecastViewModel.HourlyItems);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Android.Util.Log.Error("ForecastActivity", $"Error loading weather data: {ex.Message}");
        }
    }
}
