using _Microsoft.Android.Resource.Designer;
using Android.App;
using Android.OS;
using Android.Widget;
using NNChallenge.Constants;
using NNChallenge.Droid;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid.Views;

[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
public class MainActivity : BaseAppCompatActivity<LocationViewModel>
{
    protected override void InitializeView(Bundle? savedInstanceState)
    {
        SetContentView(Resource.Layout.activity_location);

        var buttonForecast = FindViewById<Button>(Resource.Id.button_forecast);
        if (buttonForecast != null)
        {
            buttonForecast.Click += OnForecastClick;
        }

        var spinnerLocations = FindViewById<Spinner>(Resource.Id.spinner_location);
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
        var spinnerLocations = FindViewById<Spinner>(Resource.Id.spinner_location);
        var selectedLocation = spinnerLocations?.SelectedItem?.ToString() ?? string.Empty;

        if (!string.IsNullOrEmpty(selectedLocation))
        {
            _ = ViewModel.SelectLocationCommand.ExecuteAsync(selectedLocation);
        }
    }
}
