using System.ComponentModel;
using Android.Views;
using NNChallenge.Constants;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid.Views;

[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
public class MainActivity : BaseAppCompatView<LocationViewModel>
{
    private View _weatherContainer = null!;
    private TextView _weatherLocationLabel = null!;
    private TextView _temperatureLabel = null!;
    private TextView _conditionLabel = null!;
    private TextView _descriptionLabel = null!;
    private View _loadingOverlay = null!;
    private ProgressBar _progressBar = null!;
    private TextView _loadingLabel = null!;

    protected override void InitializeView(Bundle? savedInstanceState)
    {
        SetContentView(Resource.Layout.activity_location);

        // Setup toolbar
        var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
        if (toolbar != null)
        {
            SetSupportActionBar(toolbar);
            if (SupportActionBar != null)
                SupportActionBar.Title = "Location";
        }

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

        SetupWeatherWidget();
        SetupLoadingIndicator();

        ViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void SetupWeatherWidget()
    {
        _weatherContainer = FindViewById<View>(Resource.Id.weather_container);
        _weatherLocationLabel = FindViewById<TextView>(Resource.Id.weather_location_label);
        _temperatureLabel = FindViewById<TextView>(Resource.Id.temperature_label);
        _conditionLabel = FindViewById<TextView>(Resource.Id.condition_label);
        _descriptionLabel = FindViewById<TextView>(Resource.Id.description_label);

        if (_weatherContainer != null)
        {
            _weatherContainer.Visibility = ViewStates.Gone;
        }
    }

    private void SetupLoadingIndicator()
    {
        _loadingOverlay = FindViewById<View>(Resource.Id.loading_overlay);
        _progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar);
        _loadingLabel = FindViewById<TextView>(Resource.Id.loading_label);

        if (_loadingOverlay != null)
        {
            _loadingOverlay.Visibility = ViewStates.Gone;
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.WeatherData))
        {
            RunOnUiThread(UpdateWeatherDisplay);
        }
        else if (e.PropertyName == nameof(ViewModel.IsBusy))
        {
            RunOnUiThread(UpdateLoadingIndicator);
        }
    }

    private void UpdateWeatherDisplay()
    {
        var weatherData = ViewModel.WeatherData;

        if (weatherData?.Current is not null && _weatherContainer != null)
        {
            if (_weatherLocationLabel != null)
                _weatherLocationLabel.Text = $"📍 {weatherData.Location.Name}";
            if (_temperatureLabel != null)
                _temperatureLabel.Text =
                    $"{weatherData.Current.TempC:F1}°C / {weatherData.Current.TempF:F1}°F";
            if (_conditionLabel != null)
                _conditionLabel.Text = weatherData.Current.Condition.Text;
            if (_descriptionLabel != null)
                _descriptionLabel.Text =
                    $"Feels like {weatherData.Current.FeelslikeC:F1}°C • Humidity: {weatherData.Current.Humidity}%";

            _weatherContainer.Visibility = ViewStates.Visible;
        }
        else if (_weatherContainer != null)
        {
            _weatherContainer.Visibility = ViewStates.Gone;
        }
    }

    private void UpdateLoadingIndicator()
    {
        if (_loadingOverlay == null)
            return;

        if (ViewModel.IsBusy)
        {
            if (_loadingLabel != null)
                _loadingLabel.Text = ViewModel.Title;
            _loadingOverlay.Visibility = ViewStates.Visible;
        }
        else
        {
            _loadingOverlay.Visibility = ViewStates.Gone;
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

    protected override void OnDestroy()
    {
        ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        base.OnDestroy();
    }
}
