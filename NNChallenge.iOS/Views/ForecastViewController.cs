using System.ComponentModel;
using NNChallenge.Models.DAO;
using NNChallenge.ViewModels;

namespace NNChallenge.iOS.Views;

public partial class ForecastViewController(WeatherDataDAO weatherData)
    : BaseViewController<ForecastViewModel, WeatherDataDAO>(
        nameof(ForecastViewController),
        null!,
        weatherData
    )
{
    private UICollectionView _collectionView = null!;
    private HourlyForecastDataSource _dataSource = null!;
    private UIRefreshControl _refreshControl = null!;
    private string _currentLocation = string.Empty;

    protected override void InitializeView()
    {
        Title = !string.IsNullOrEmpty(Parameter?.Location?.Name)
            ? $"{Parameter.Location.Name} Forecast"
            : "3-Day Hourly Forecast";

        _currentLocation = Parameter?.Location?.Name ?? string.Empty;

        SetupCollectionView();
        SetupRefreshControl();
        SetupNavigationBar();

        ViewModel.PropertyChanged += OnViewModelPropertyChanged;
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

    private void SetupRefreshControl()
    {
        _refreshControl = new UIRefreshControl();
        _refreshControl.ValueChanged += OnRefreshRequested;

        _refreshControl.TintColor = UIColor.FromRGB(52, 152, 219); // Blue color
        _refreshControl.AttributedTitle = new NSAttributedString(
            "Pull to refresh weather data...",
            new UIStringAttributes { ForegroundColor = UIColor.FromRGB(52, 152, 219) }
        );

        _collectionView.RefreshControl = _refreshControl;
    }

    private void SetupNavigationBar()
    {
        var refreshButton = new UIBarButtonItem(
            UIBarButtonSystemItem.Refresh,
            async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(_currentLocation))
                {
                    await ViewModel.LoadForecastCommand.ExecuteAsync(_currentLocation);
                }
            }
        );

        NavigationItem.RightBarButtonItem = refreshButton;
    }

    private async void OnRefreshRequested(object? sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_currentLocation))
        {
            await ViewModel.LoadForecastCommand.ExecuteAsync(_currentLocation);
        }

        _refreshControl.EndRefreshing();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.IsBusy))
        {
            InvokeOnMainThread(() =>
            {
                if (_refreshControl != null)
                {
                    if (ViewModel.IsBusy)
                    {
                        if (!_refreshControl.Refreshing)
                        {
                            _refreshControl.BeginRefreshing();
                        }
                    }
                    else
                    {
                        _refreshControl.EndRefreshing();
                    }
                }
            });
        }
        else if (e.PropertyName == nameof(ViewModel.HourlyItems))
        {
            InvokeOnMainThread(() =>
            {
                _dataSource?.UpdateData(ViewModel.HourlyItems);
                _collectionView?.ReloadData();
            });
        }
    }

    public override void DidReceiveMemoryWarning()
    {
        base.DidReceiveMemoryWarning();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            }

            if (_refreshControl != null)
            {
                _refreshControl.ValueChanged -= OnRefreshRequested;
                _refreshControl.Dispose();
            }
        }

        base.Dispose(disposing);
    }
}
