using System.Text.Json;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Droid.Views;
using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;
using AndroidX.AppCompat.Widget;
using Android.Views;

namespace NNChallenge.Droid;

[Activity(Label = "ForecastActivity", Theme = "@style/AppTheme.NoActionBar")]
public class ForecastActivity : BaseAppCompatView<ForecastViewModel, WeatherDataDAO>
{
    private RecyclerView _recyclerView = null!;
    private HourlyForecastAdapter _adapter = null!;

    protected override void InitializeView(Bundle? savedInstanceState)
    {
        SetContentView(Resource.Layout.forecast_activity);

        // Setup toolbar with back button
        var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
        if (toolbar != null)
        {
            SetSupportActionBar(toolbar);
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar?.SetDisplayShowHomeEnabled(true);
        }

        LoadWeatherData();
        SetupRecyclerView();
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
        if (item.ItemId == Android.Resource.Id.Home)
        {
            Finish();
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

    private void LoadWeatherData()
    {
        var weatherDataJson = Intent?.GetStringExtra("WeatherData");
        if (!string.IsNullOrEmpty(weatherDataJson))
        {
            var weatherData = JsonSerializer.Deserialize<WeatherDataDAO>(weatherDataJson);
            if (weatherData is not null)
            {
                // Set the title with city name
                var cityName = weatherData.Location?.Name ?? "Unknown";
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
}
