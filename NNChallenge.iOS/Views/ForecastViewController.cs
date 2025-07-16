using NNChallenge.Core;
using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;

namespace NNChallenge.iOS.Views;

public partial class ForecastViewController : BaseViewController
{
    private ForecastViewModel _viewModel;
    private WeatherDataDAO? _weatherData;
    private UICollectionView _collectionView = null!;
    private HourlyForecastDataSource _dataSource = null!;

    public ForecastViewController()
        : base(nameof(ForecastViewController), null)
    {
        _viewModel = App.GetService<ForecastViewModel>();
    }

    public ForecastViewController(WeatherDataDAO weatherData)
        : base(nameof(ForecastViewController), null)
    {
        _viewModel = App.GetService<ForecastViewModel>();
        _weatherData = weatherData;
    }

    protected override void InitializeView()
    {
        Title = !string.IsNullOrEmpty(_weatherData?.Location?.Name)
            ? $"{_weatherData.Location.Name} Forecast"
            : "3-Day Hourly Forecast";

        SetupCollectionView();
        SetWeatherData(_weatherData);
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

        _dataSource = new HourlyForecastDataSource(_viewModel.HourlyItems);

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

    void SetWeatherData(WeatherDataDAO weatherData)
    {
        _viewModel.SetWeatherData(weatherData);

        if (_collectionView is not null && _dataSource is not null)
        {
            _dataSource.UpdateData(_viewModel.HourlyItems);
            _collectionView.ReloadData();
        }
    }

    public override void DidReceiveMemoryWarning()
    {
        base.DidReceiveMemoryWarning();
    }
}
