using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;

namespace NNChallenge.iOS.Views;

public partial class ForecastViewController(WeatherDataDAO weatherData)
    : BaseViewController<ForecastViewModel, WeatherDataDAO>(
        nameof(ForecastViewController),
        null,
        weatherData
    )
{
    private UICollectionView _collectionView = null!;
    private HourlyForecastDataSource _dataSource = null!;

    protected override void InitializeView()
    {
        Title = !string.IsNullOrEmpty(Parameter?.Location?.Name)
            ? $"{Parameter.Location.Name} Forecast"
            : "3-Day Hourly Forecast";

        SetupCollectionView();
    }

    void SetupCollectionView()
    {
        var layout = new UICollectionViewFlowLayout
        {
            ItemSize = new CGSize(UIScreen.MainScreen.Bounds.Width - 32, 160),
            MinimumInteritemSpacing = 8,
            MinimumLineSpacing = 12,
            SectionInset = new UIEdgeInsets(16, 16, 16, 16),
        };

        _dataSource = new HourlyForecastDataSource(ViewModel.HourlyItems);

        _collectionView = new UICollectionView(CGRect.Empty, layout)
        {
            BackgroundColor = UIColor.FromRGB(245, 250, 255),
            DataSource = _dataSource,
            Delegate = new HourlyForecastDelegate(),
        };

        _collectionView.RegisterClassForCell(
            typeof(HourlyForecastCell),
            HourlyForecastCell.CellIdentifier
        );

        View!.AddSubview(_collectionView);

        _collectionView.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints(
            [
                _collectionView.TopAnchor.ConstraintEqualTo(View!.SafeAreaLayoutGuide.TopAnchor),
                _collectionView.LeadingAnchor.ConstraintEqualTo(View!.LeadingAnchor),
                _collectionView.TrailingAnchor.ConstraintEqualTo(View!.TrailingAnchor),
                _collectionView.BottomAnchor.ConstraintEqualTo(
                    View!.SafeAreaLayoutGuide.BottomAnchor
                ),
            ]
        );
    }

    public override void DidReceiveMemoryWarning()
    {
        base.DidReceiveMemoryWarning();
    }
}
