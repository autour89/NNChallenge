using _Microsoft.Android.Resource.Designer;
using Android.Content;
using AndroidX.AppCompat.App;
using NNChallenge.Constants;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid;

[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
public class MainActivity : AppCompatActivity
{
    private LocationViewModel? _locationViewModel;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(ResourceConstant.Layout.activity_location);

        _locationViewModel = ServiceProvider.GetService<LocationViewModel>();

        Button buttonForecst = FindViewById<Button>(ResourceConstant.Id.button_forecast)!;
        buttonForecst.Click += OnForecastClick;

        Spinner spinnerLocations = FindViewById<Spinner>(ResourceConstant.Id.spinner_location)!;

        ArrayAdapter<string> adapter =
            new(
                this,
                Android.Resource.Layout.SimpleSpinnerDropDownItem,
                LocationConstants.LOCATIONS
            );

        adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

        spinnerLocations.Adapter = adapter;
    }

    private void OnForecastClick(object? sender, EventArgs e)
    {
        // Get selected location and pass it to the ViewModel
        Spinner spinnerLocations = FindViewById<Spinner>(ResourceConstant.Id.spinner_location)!;
        string selectedLocation = spinnerLocations.SelectedItem?.ToString() ?? string.Empty;

        if (!string.IsNullOrEmpty(selectedLocation) && _locationViewModel != null)
        {
            // Execute the location selection command
            _locationViewModel.SelectLocationCommand.Execute(selectedLocation);
        }

        this.StartActivity(new Intent(this, typeof(Views.ForecastActivity)));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
