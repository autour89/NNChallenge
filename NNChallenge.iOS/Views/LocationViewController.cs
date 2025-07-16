using System.ComponentModel;
using NNChallenge.Constants;
using NNChallenge.Core;
using NNChallenge.ViewModels;

namespace NNChallenge.iOS.Views;

public partial class LocationViewController : BaseViewController
{
    private readonly LocationViewModel _viewModel;

    private UILabel _weatherLocationLabel = null!;
    private UILabel _temperatureLabel = null!;
    private UILabel _conditionLabel = null!;
    private UILabel _descriptionLabel = null!;
    private UIView _weatherContainer = null!;
    private UIView _loadingOverlay = null!;
    private UIActivityIndicatorView _activityIndicator = null!;
    private UILabel _loadingLabel = null!;

    public LocationViewController()
        : base(nameof(LocationViewController), null)
    {
        _viewModel = App.GetService<LocationViewModel>();
    }

    protected override void InitializeView()
    {
        Title = "Location";
        _submitButton.TitleLabel.Text = "Submit";
        _contentLabel.Text = "Select your location.";
        _submitButton.TouchUpInside += SubmitButtonTouchUpInside;

        _picker.Model = new LocationPickerModel(LocationConstants.LOCATIONS);

        _weatherContainer = new UIView
        {
            BackgroundColor = UIColor.FromRGB(240, 248, 255),
            Layer =
            {
                CornerRadius = 12,
                BorderWidth = 1,
                BorderColor = UIColor.FromRGB(200, 230, 255).CGColor,
            },
            Hidden = true,
        };

        _weatherLocationLabel = new UILabel
        {
            Font = UIFont.BoldSystemFontOfSize(18),
            TextAlignment = UITextAlignment.Center,
            TextColor = UIColor.FromRGB(51, 51, 51),
            Lines = 1,
        };

        _temperatureLabel = new UILabel
        {
            Font = UIFont.SystemFontOfSize(24),
            TextAlignment = UITextAlignment.Center,
            TextColor = UIColor.FromRGB(0, 122, 255),
            Lines = 1,
        };

        _conditionLabel = new UILabel
        {
            Font = UIFont.SystemFontOfSize(16),
            TextAlignment = UITextAlignment.Center,
            TextColor = UIColor.FromRGB(102, 102, 102),
            Lines = 1,
        };

        _descriptionLabel = new UILabel
        {
            Font = UIFont.SystemFontOfSize(14),
            TextAlignment = UITextAlignment.Center,
            TextColor = UIColor.FromRGB(153, 153, 153),
            Lines = 2,
        };

        _weatherContainer.AddSubviews(
            _weatherLocationLabel,
            _temperatureLabel,
            _conditionLabel,
            _descriptionLabel
        );

        View!.AddSubview(_weatherContainer);
        SetupWeatherConstraints();
        SetupLoadingIndicator();

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void SetupLoadingIndicator()
    {
        _loadingOverlay = new UIView
        {
            BackgroundColor = UIColor.Black.ColorWithAlpha(0.5f),
            Hidden = true,
            Alpha = 0.0f,
        };

        _activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Large)
        {
            Color = UIColor.White,
            HidesWhenStopped = true,
        };

        _loadingLabel = new UILabel
        {
            Text = "Loading weather data...",
            TextColor = UIColor.White,
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.SystemFontOfSize(16),
            Lines = 0,
        };

        _loadingOverlay.AddSubviews(_activityIndicator, _loadingLabel);
        View!.AddSubview(_loadingOverlay);

        _loadingOverlay.TranslatesAutoresizingMaskIntoConstraints = false;
        _activityIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
        _loadingLabel.TranslatesAutoresizingMaskIntoConstraints = false;

        NSLayoutConstraint.ActivateConstraints(
            [
                _loadingOverlay.TopAnchor.ConstraintEqualTo(View!.TopAnchor),
                _loadingOverlay.LeadingAnchor.ConstraintEqualTo(View!.LeadingAnchor),
                _loadingOverlay.TrailingAnchor.ConstraintEqualTo(View!.TrailingAnchor),
                _loadingOverlay.BottomAnchor.ConstraintEqualTo(View!.BottomAnchor),
                _activityIndicator.CenterXAnchor.ConstraintEqualTo(_loadingOverlay.CenterXAnchor),
                _activityIndicator.CenterYAnchor.ConstraintEqualTo(_loadingOverlay.CenterYAnchor),
                _loadingLabel.TopAnchor.ConstraintEqualTo(_activityIndicator.BottomAnchor, 16),
                _loadingLabel.LeadingAnchor.ConstraintEqualTo(_loadingOverlay.LeadingAnchor, 20),
                _loadingLabel.TrailingAnchor.ConstraintEqualTo(_loadingOverlay.TrailingAnchor, -20),
            ]
        );
    }

    private void SetupWeatherConstraints()
    {
        _weatherContainer.TranslatesAutoresizingMaskIntoConstraints = false;
        _weatherLocationLabel.TranslatesAutoresizingMaskIntoConstraints = false;
        _temperatureLabel.TranslatesAutoresizingMaskIntoConstraints = false;
        _conditionLabel.TranslatesAutoresizingMaskIntoConstraints = false;
        _descriptionLabel.TranslatesAutoresizingMaskIntoConstraints = false;

        NSLayoutConstraint.ActivateConstraints(
            [
                _weatherContainer.CenterXAnchor.ConstraintEqualTo(View!.CenterXAnchor),
                _weatherContainer.CenterYAnchor.ConstraintEqualTo(View!.CenterYAnchor, 50),
                _weatherContainer.LeadingAnchor.ConstraintGreaterThanOrEqualTo(
                    View!.LeadingAnchor,
                    20
                ),
                _weatherContainer.TrailingAnchor.ConstraintLessThanOrEqualTo(
                    View!.TrailingAnchor,
                    -20
                ),
                _weatherContainer.HeightAnchor.ConstraintEqualTo(180),
                _weatherContainer.WidthAnchor.ConstraintLessThanOrEqualTo(350),
                _weatherLocationLabel.TopAnchor.ConstraintEqualTo(_weatherContainer.TopAnchor, 15),
                _weatherLocationLabel.LeadingAnchor.ConstraintEqualTo(
                    _weatherContainer.LeadingAnchor,
                    15
                ),
                _weatherLocationLabel.TrailingAnchor.ConstraintEqualTo(
                    _weatherContainer.TrailingAnchor,
                    -15
                ),
                _temperatureLabel.TopAnchor.ConstraintEqualTo(
                    _weatherLocationLabel.BottomAnchor,
                    15
                ),
                _temperatureLabel.LeadingAnchor.ConstraintEqualTo(
                    _weatherContainer.LeadingAnchor,
                    15
                ),
                _temperatureLabel.TrailingAnchor.ConstraintEqualTo(
                    _weatherContainer.TrailingAnchor,
                    -15
                ),
                _conditionLabel.TopAnchor.ConstraintEqualTo(_temperatureLabel.BottomAnchor, 10),
                _conditionLabel.LeadingAnchor.ConstraintEqualTo(
                    _weatherContainer.LeadingAnchor,
                    15
                ),
                _conditionLabel.TrailingAnchor.ConstraintEqualTo(
                    _weatherContainer.TrailingAnchor,
                    -15
                ),
                _descriptionLabel.TopAnchor.ConstraintEqualTo(_conditionLabel.BottomAnchor, 10),
                _descriptionLabel.LeadingAnchor.ConstraintEqualTo(
                    _weatherContainer.LeadingAnchor,
                    15
                ),
                _descriptionLabel.TrailingAnchor.ConstraintEqualTo(
                    _weatherContainer.TrailingAnchor,
                    -15
                ),
                _descriptionLabel.BottomAnchor.ConstraintLessThanOrEqualTo(
                    _weatherContainer.BottomAnchor,
                    -15
                ),
            ]
        );
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.WeatherData))
        {
            InvokeOnMainThread(UpdateWeatherDisplay);
        }
        else if (e.PropertyName == nameof(_viewModel.IsBusy))
        {
            InvokeOnMainThread(UpdateLoadingIndicator);
        }
    }

    private void UpdateWeatherDisplay()
    {
        var weatherData = _viewModel.WeatherData;

        if (weatherData?.Current is not null)
        {
            _weatherLocationLabel.Text = $"📍 {weatherData.Location.Name}";
            _temperatureLabel.Text =
                $"{weatherData.Current.TempC:F1}°C / {weatherData.Current.TempF:F1}°F";
            _conditionLabel.Text = weatherData.Current.Condition.Text;
            _descriptionLabel.Text =
                $"Feels like {weatherData.Current.FeelslikeC:F1}°C • Humidity: {weatherData.Current.Humidity}%";

            _weatherContainer.Hidden = false;

            UIView.Animate(
                0.3,
                () =>
                {
                    _weatherContainer.Alpha = 1.0f;
                }
            );
        }
        else
        {
            _weatherContainer.Hidden = true;
            _weatherContainer.Alpha = 0.0f;
        }
    }

    private void UpdateLoadingIndicator()
    {
        if (_viewModel.IsBusy)
        {
            _loadingLabel.Text = _viewModel.Title;

            _loadingOverlay.Hidden = false;
            _activityIndicator.StartAnimating();

            UIView.Animate(
                0.3,
                () =>
                {
                    _loadingOverlay.Alpha = 1.0f;
                }
            );
        }
        else
        {
            UIView.Animate(
                0.3,
                () =>
                {
                    _loadingOverlay.Alpha = 0.0f;
                },
                () =>
                {
                    _loadingOverlay.Hidden = true;
                    _activityIndicator.StopAnimating();
                }
            );
        }
    }

    private void SubmitButtonTouchUpInside(object? sender, EventArgs e)
    {
        var selected = _picker.SelectedRowInComponent(0);
        var selectedLocation = LocationConstants.LOCATIONS[selected];

        _ = _viewModel.SelectLocationCommand.ExecuteAsync(selectedLocation);
    }

    public override void DidReceiveMemoryWarning()
    {
        base.DidReceiveMemoryWarning();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
        base.Dispose(disposing);
    }
}
