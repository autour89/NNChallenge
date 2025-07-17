using System.Text.Json;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Droid.Views;
using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;

namespace NNChallenge.Droid;

[Activity(Label = "ForecastActivity")]
public class ForecastActivity : BaseView<ForecastViewModel, WeatherDataDAO>
{
    private RecyclerView _recyclerView = null!;
    private HourlyForecastAdapter _adapter = null!;

    protected override void InitializeView(Bundle? savedInstanceState)
    {
        SetContentView(Resource.Layout.forecast_activity);

        LoadWeatherData();
        SetupRecyclerView();
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
                if (!string.IsNullOrEmpty(weatherData.Location?.Name))
                {
                    Title = $"{weatherData.Location.Name} Forecast";
                }
                else
                {
                    Title = "3-Day Hourly Forecast";
                }

                SetParameter(weatherData);

                _adapter?.UpdateData(ViewModel.HourlyItems);
            }
        }
    }
}
