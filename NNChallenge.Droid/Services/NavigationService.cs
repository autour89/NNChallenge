using System.Text.Json;
using Android.Content;
using NNChallenge.Constants;
using NNChallenge.Models.DAO;
using NNChallenge.Services;

namespace NNChallenge.Droid.Services;

public class NavigationService : INavigationService
{
    private static Context Context =>
        WeatherApplication.CurrentContext
        ?? throw new InvalidOperationException("Application context is not initialized");

    public static Activity? CurrentActivity { get; set; }

    public void NavigateTo<TParameter>(ScreenType screenType, TParameter? parameter = null)
        where TParameter : class
    {
        Intent? intent = null;

        switch (screenType)
        {
            case ScreenType.Forecast:
                if (parameter is WeatherDataDAO weatherData)
                {
                    intent = new Intent(Context, typeof(ForecastActivity));

                    var weatherDataJson = JsonSerializer.Serialize(weatherData);
                    intent.PutExtra("WeatherData", weatherDataJson);
                }
                break;

            default:
                return;
        }

        if (intent is null)
        {
            return;
        }

        if (CurrentActivity != null)
        {
            CurrentActivity.StartActivity(intent);
        }
        else
        {
            intent.AddFlags(ActivityFlags.NewTask);
            Context.StartActivity(intent);
        }
    }

    public void GoBack()
    {
        CurrentActivity?.Finish();
    }
}
