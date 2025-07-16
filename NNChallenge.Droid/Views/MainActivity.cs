using System.Text.Json;
using _Microsoft.Android.Resource.Designer;
using Android.Content;
using AndroidX.AppCompat.App;
using NNChallenge.Constants;
using NNChallenge.Core;
using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid.Views;

[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
public class MainActivity : AppCompatActivity
{
    private LocationViewModel? _locationViewModel;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(ResourceConstant.Layout.activity_location);

        _locationViewModel = App.GetService<LocationViewModel>();

        if (_locationViewModel != null)
        {
            _locationViewModel.OnWeatherDataLoaded = NavigateToForecastView;
        }

        var buttonForecast = FindViewById<Button>(ResourceConstant.Id.button_forecast);
        if (buttonForecast != null)
        {
            buttonForecast.Click += OnForecastClick;
        }

        var spinnerLocations = FindViewById<Spinner>(ResourceConstant.Id.spinner_location);
        if (spinnerLocations != null)
        {
            var adapter = new ArrayAdapter<string>(
                this,
                Android.Resource.Layout.SimpleSpinnerDropDownItem,
                LocationConstants.LOCATIONS
            );

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerLocations.Adapter = adapter;
        }
    }

    private void OnForecastClick(object? sender, EventArgs e)
    {
        var spinnerLocations = FindViewById<Spinner>(ResourceConstant.Id.spinner_location);
        var selectedLocation = spinnerLocations?.SelectedItem?.ToString() ?? string.Empty;

        if (!string.IsNullOrEmpty(selectedLocation) && _locationViewModel is not null)
        {
            _ = _locationViewModel.SelectLocationCommand.ExecuteAsync(selectedLocation);
        }
    }

    private void NavigateToForecastView(WeatherDataDAO weatherData)
    {
        var intent = new Intent(this, typeof(ForecastActivity));
        var weatherDataJson = JsonSerializer.Serialize(weatherData);
        intent.PutExtra("WeatherData", weatherDataJson);
        StartActivity(intent);
    }
}
