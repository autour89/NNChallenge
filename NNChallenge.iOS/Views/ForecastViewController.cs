using NNChallenge.ViewModels;

namespace NNChallenge.iOS.Views;

public partial class ForecastViewController : UIViewController
{
    private ForecastViewModel _viewModel;

    public ForecastViewController()
        : base("ForecastViewController", null)
    {
        _viewModel = ServiceProvider.GetService<ForecastViewModel>();
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        Title = "Forecast";

        // Perform any additional setup after loading the view, typically from a nib.
    }

    public override void DidReceiveMemoryWarning()
    {
        base.DidReceiveMemoryWarning();
        // Release any cached data, images, etc that aren't in use.
    }
}
