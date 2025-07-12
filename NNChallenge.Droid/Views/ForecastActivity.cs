using NNChallenge.ViewModels;

namespace NNChallenge.Droid.Views;

[Activity(Label = "ForecastActivity")]
public class ForecastActivity : Activity
{
    private ForecastViewModel? _forecastViewModel;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        _forecastViewModel = ServiceProvider.GetService<ForecastViewModel>();
    }
}
