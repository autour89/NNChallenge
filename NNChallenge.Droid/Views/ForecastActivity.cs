using System.ComponentModel;
using System.Text.Json;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using NNChallenge.Droid.Views;
using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid;

[Activity(Label = "ForecastActivity", Theme = "@style/AppTheme.NoActionBar")]
public class ForecastActivity : BaseAppCompatView<ForecastViewModel, WeatherDataDAO>
{
    private RecyclerView _recyclerView = null!;
    private HourlyForecastAdapter _adapter = null!;
    private SwipeRefreshLayout _swipeRefreshLayout = null!;
    private string _currentLocation = string.Empty;

    protected override void InitializeView(Bundle? savedInstanceState)
    {
        SetContentView(Resource.Layout.forecast_activity);

        var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
        if (toolbar != null)
        {
            SetSupportActionBar(toolbar);
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar?.SetDisplayShowHomeEnabled(true);
        }

        LoadWeatherData();
        SetupSwipeRefresh();
        SetupRecyclerView();

        ViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool OnCreateOptionsMenu(IMenu? menu)
    {
        MenuInflater.Inflate(Resource.Menu.forecast_menu, menu);
        return true;
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
        if (item.ItemId == Android.Resource.Id.Home)
        {
            Finish();
            return true;
        }
        else if (item.ItemId == Resource.Id.action_refresh)
        {
            if (!string.IsNullOrEmpty(_currentLocation))
            {
                _ = ViewModel.LoadForecastCommand.ExecuteAsync(_currentLocation);
            }
            return true;
        }
        return base.OnOptionsItemSelected(item);
    }

    private void SetupRecyclerView()
    {
        _recyclerView = FindViewById<RecyclerView>(Resource.Id.hourly_forecast_recycler)!;
        _recyclerView.SetLayoutManager(new LinearLayoutManager(this));

        _adapter = new HourlyForecastAdapter(ViewModel.HourlyItems);
        _recyclerView.SetAdapter(_adapter);
    }

    private void SetupSwipeRefresh()
    {
        _swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_refresh_layout)!;
        _swipeRefreshLayout.Refresh += OnRefresh;

        _swipeRefreshLayout.SetColorSchemeResources(
            Android.Resource.Color.HoloBlueLight,
            Android.Resource.Color.HoloGreenLight,
            Android.Resource.Color.HoloOrangeLight,
            Android.Resource.Color.HoloRedLight
        );
    }

    private void OnRefresh(object? sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_currentLocation))
        {
            _ = ViewModel.LoadForecastCommand.ExecuteAsync(_currentLocation);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.IsBusy))
        {
            RunOnUiThread(() =>
            {
                if (_swipeRefreshLayout != null)
                {
                    _swipeRefreshLayout.Refreshing = ViewModel.IsBusy;
                }
            });
        }
        else if (e.PropertyName == nameof(ViewModel.HourlyItems))
        {
            RunOnUiThread(() =>
            {
                _adapter?.UpdateData(ViewModel.HourlyItems);
            });
        }
    }

    private void LoadWeatherData()
    {
        var weatherDataJson = Intent?.GetStringExtra("WeatherData");
        if (!string.IsNullOrEmpty(weatherDataJson))
        {
            var weatherData = JsonSerializer.Deserialize<WeatherDataDAO>(weatherDataJson);
            if (weatherData is not null)
            {
                var cityName = weatherData.Location?.Name ?? "Unknown";
                _currentLocation = cityName;

                if (SupportActionBar != null)
                    SupportActionBar.Title = $"{cityName} Forecast";

                SetParameter(weatherData);

                _adapter?.UpdateData(ViewModel.HourlyItems);
            }
        }
        else
        {
            if (SupportActionBar != null)
                SupportActionBar.Title = "3-Day Hourly Forecast";
        }
    }

    protected override void OnDestroy()
    {
        if (ViewModel != null)
        {
            ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        if (_swipeRefreshLayout != null)
        {
            _swipeRefreshLayout.Refresh -= OnRefresh;
        }

        base.OnDestroy();
    }
}
