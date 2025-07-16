using Microsoft.Extensions.Logging;
using NNChallenge.Constants;
using NNChallenge.iOS.Views;
using NNChallenge.Models.DAO;
using NNChallenge.Services;

namespace NNChallenge.iOS.Services;

public class NavigationService(ILogger<NavigationService> logger) : INavigationService
{
    private readonly ILogger<NavigationService> _logger = logger;

    public void NavigateTo<TParameter>(ScreenType screenType, TParameter parameter)
        where TParameter : class
    {
        if (AppDelegate.NavigationController is null)
        {
            return;
        }

        UIViewController? viewController = null;

        switch (screenType)
        {
            case ScreenType.Forecast:
                if (parameter is WeatherDataDAO weatherData)
                {
                    viewController = new ForecastViewController(weatherData);
                }
                break;
            default:
                _logger.LogError("Unknown screen type: {ScreenType}", screenType);
                return;
        }

        if (viewController == null)
        {
            return;
        }

        UIApplication.SharedApplication.InvokeOnMainThread(() =>
        {
            AppDelegate.NavigationController.PushViewController(viewController, true);
        });
    }

    public void NavigateTo(ScreenType screenType)
    {
        if (AppDelegate.NavigationController is null)
        {
            return;
        }

        UIViewController? viewController = null;

        UIApplication.SharedApplication.InvokeOnMainThread(() =>
        {
            AppDelegate.NavigationController.PushViewController(viewController, true);
        });
    }

    public void GoBack()
    {
        if (AppDelegate.NavigationController is null)
        {
            return;
        }

        UIApplication.SharedApplication.InvokeOnMainThread(() =>
        {
            AppDelegate.NavigationController.PopViewController(true);
        });
    }
}
