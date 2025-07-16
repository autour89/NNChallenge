using NNChallenge.Constants;
using NNChallenge.iOS.Views;
using NNChallenge.Models.DAO;
using NNChallenge.Services;
using UIKit;

namespace NNChallenge.iOS.Services;

public class NavigationService : INavigationService
{
    public void NavigateTo<TParameter>(ScreenType screenType, TParameter? parameter = null)
        where TParameter : class
    {
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
                return;
        }

        if (AppDelegate.NavigationController is null || viewController is null)
        {
            return;
        }

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
