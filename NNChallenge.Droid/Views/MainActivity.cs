using _Microsoft.Android.Resource.Designer;
using AndroidX.AppCompat.App;
using NNChallenge.Constants;
using NNChallenge.Core;
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
}
