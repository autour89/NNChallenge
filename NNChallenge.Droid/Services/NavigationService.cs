using System.Text.Json;
using Android.Content;
using NNChallenge.Constants;
using NNChallenge.Droid.Views;
using NNChallenge.Models.DAO;
using NNChallenge.Services;

namespace NNChallenge.Droid.Services;

public class NavigationService(Context context) : INavigationService
{
    private readonly Context _context = context;

    public void NavigateTo<TParameter>(ScreenType screenType, TParameter? parameter = null)
        where TParameter : class
    {
        Intent? intent = null;

        switch (screenType)
        {
            case ScreenType.Forecast:
                if (parameter is WeatherDataDAO weatherData)
                {
                    intent = new Intent(_context, typeof(ForecastActivity));

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

        _context.StartActivity(intent);
    }

    public void GoBack()
    {
        if (_context is Activity activity)
        {
            activity.Finish();
        }
    }
}
