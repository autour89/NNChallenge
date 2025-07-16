using NNChallenge.Models.DAO;
using UIKit;

namespace NNChallenge.iOS.Views;

public class HourlyForecastCell : UICollectionViewCell
{
    public static readonly string CellIdentifier = "HourlyForecastCell";

    private UILabel _dateLabel = null!;
    private UILabel _timeLabel = null!;
    private UILabel _conditionLabel = null!;
    private UILabel _humidityLabel = null!;
    private UILabel _temperatureLabel = null!;
    private UIView _containerView = null!;
    private UIView _leftContainer = null!;
    private UIView _rightContainer = null!;

    public HourlyForecastCell(IntPtr handle) : base(handle)
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        // Create container with rounded corners and border
        _containerView = new UIView
        {
            BackgroundColor = UIColor.FromRGB(240, 248, 255), // Light blue background like main screen
            Layer =
            {
                CornerRadius = 12, // Increased corner radius
                BorderWidth = 1,
                BorderColor = UIColor.FromRGB(200, 230, 255).CGColor, // Light blue border
                MasksToBounds = true
            }
        };

        // Create left container for date, time, condition, humidity
        _leftContainer = new UIView();
        
        // Create right container for temperature
        _rightContainer = new UIView();

        // Date label
        _dateLabel = new UILabel
        {
            Font = UIFont.BoldSystemFontOfSize(16),
            TextAlignment = UITextAlignment.Left,
            TextColor = UIColor.FromRGB(68, 68, 68),
            Lines = 1,
        };

        // Time label
        _timeLabel = new UILabel
        {
            Font = UIFont.BoldSystemFontOfSize(18),
            TextAlignment = UITextAlignment.Left,
            TextColor = UIColor.FromRGB(51, 51, 51),
            Lines = 1,
        };

        // Condition label
        _conditionLabel = new UILabel
        {
            Font = UIFont.SystemFontOfSize(16),
            TextAlignment = UITextAlignment.Left,
            TextColor = UIColor.FromRGB(102, 102, 102),
            Lines = 1,
        };

        // Humidity label
        _humidityLabel = new UILabel
        {
            Font = UIFont.SystemFontOfSize(14),
            TextAlignment = UITextAlignment.Left,
            TextColor = UIColor.FromRGB(153, 153, 153),
            Lines = 1,
        };

        // Temperature label (right side, centered vertically)
        _temperatureLabel = new UILabel
        {
            Font = UIFont.BoldSystemFontOfSize(24),
            TextAlignment = UITextAlignment.Center,
            TextColor = UIColor.FromRGB(0, 122, 255),
            Lines = 1,
        };

        // Add labels to containers
        _leftContainer.AddSubviews(_dateLabel, _timeLabel, _conditionLabel, _humidityLabel);
        _rightContainer.AddSubview(_temperatureLabel);
        
        // Add containers to main container
        _containerView.AddSubviews(_leftContainer, _rightContainer);
        ContentView.AddSubview(_containerView);
        
        SetupConstraints();
    }

    private void SetupConstraints()
    {
        _containerView.TranslatesAutoresizingMaskIntoConstraints = false;
        _leftContainer.TranslatesAutoresizingMaskIntoConstraints = false;
        _rightContainer.TranslatesAutoresizingMaskIntoConstraints = false;
        _dateLabel.TranslatesAutoresizingMaskIntoConstraints = false;
        _timeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
        _conditionLabel.TranslatesAutoresizingMaskIntoConstraints = false;
        _humidityLabel.TranslatesAutoresizingMaskIntoConstraints = false;
        _temperatureLabel.TranslatesAutoresizingMaskIntoConstraints = false;

        NSLayoutConstraint.ActivateConstraints([
            // Container fills the cell with margins to show corner radius
            _containerView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, 8),
            _containerView.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, 8),
            _containerView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -8),
            _containerView.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -8),

            // Left container (60% width)
            _leftContainer.TopAnchor.ConstraintEqualTo(_containerView.TopAnchor, 16),
            _leftContainer.LeadingAnchor.ConstraintEqualTo(_containerView.LeadingAnchor, 16),
            _leftContainer.BottomAnchor.ConstraintEqualTo(_containerView.BottomAnchor, -16),
            _leftContainer.WidthAnchor.ConstraintEqualTo(_containerView.WidthAnchor, 0.6f, -24),

            // Right container (40% width)
            _rightContainer.TopAnchor.ConstraintEqualTo(_containerView.TopAnchor, 16),
            _rightContainer.TrailingAnchor.ConstraintEqualTo(_containerView.TrailingAnchor, -16),
            _rightContainer.BottomAnchor.ConstraintEqualTo(_containerView.BottomAnchor, -16),
            _rightContainer.LeadingAnchor.ConstraintEqualTo(_leftContainer.TrailingAnchor, 8),

            // Left container labels
            _dateLabel.TopAnchor.ConstraintEqualTo(_leftContainer.TopAnchor),
            _dateLabel.LeadingAnchor.ConstraintEqualTo(_leftContainer.LeadingAnchor),
            _dateLabel.TrailingAnchor.ConstraintEqualTo(_leftContainer.TrailingAnchor),

            _timeLabel.TopAnchor.ConstraintEqualTo(_dateLabel.BottomAnchor, 8),
            _timeLabel.LeadingAnchor.ConstraintEqualTo(_leftContainer.LeadingAnchor),
            _timeLabel.TrailingAnchor.ConstraintEqualTo(_leftContainer.TrailingAnchor),

            _conditionLabel.TopAnchor.ConstraintEqualTo(_timeLabel.BottomAnchor, 8),
            _conditionLabel.LeadingAnchor.ConstraintEqualTo(_leftContainer.LeadingAnchor),
            _conditionLabel.TrailingAnchor.ConstraintEqualTo(_leftContainer.TrailingAnchor),

            _humidityLabel.TopAnchor.ConstraintEqualTo(_conditionLabel.BottomAnchor, 8),
            _humidityLabel.LeadingAnchor.ConstraintEqualTo(_leftContainer.LeadingAnchor),
            _humidityLabel.TrailingAnchor.ConstraintEqualTo(_leftContainer.TrailingAnchor),

            // Temperature label (centered vertically in right container)
            _temperatureLabel.CenterXAnchor.ConstraintEqualTo(_rightContainer.CenterXAnchor),
            _temperatureLabel.CenterYAnchor.ConstraintEqualTo(_rightContainer.CenterYAnchor),
            _temperatureLabel.LeadingAnchor.ConstraintEqualTo(_rightContainer.LeadingAnchor),
            _temperatureLabel.TrailingAnchor.ConstraintEqualTo(_rightContainer.TrailingAnchor),
        ]);
    }

    public void ConfigureCell(HourlyForecastItem item)
    {
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] === WEATHER CELL CONFIG ===");
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] Date: {item.FormattedDate}");
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] Time: {item.FormattedTime}");
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] Temperature: {item.TempC:F1}°C");
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] Condition: {item.ConditionText}");
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] Humidity: {item.Humidity}%");
        
        // Set individual labels
        _dateLabel.Text = $"📅 {item.FormattedDate}";
        _timeLabel.Text = $"🕐 {item.FormattedTime}";
        
        // Weather condition with icon
        var weatherIcon = GetWeatherIcon(item.ConditionText);
        _conditionLabel.Text = $"{weatherIcon} {item.ConditionText}";
        
        // Humidity
        _humidityLabel.Text = $"💧 {item.Humidity}%";
        
        // Temperature (right side)
        _temperatureLabel.Text = $"{item.TempC:F1}°C";
        
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] Configured with separate labels layout");
        System.Diagnostics.Debug.WriteLine($"[HourlyForecastCell] === CONFIG COMPLETE ===");
    }
    
    private string GetWeatherIcon(string conditionText)
    {
        // Map weather conditions to emoji icons
        var condition = conditionText.ToLower();
        
        if (condition.Contains("clear") || condition.Contains("sunny"))
            return "☀️";
        else if (condition.Contains("partly cloudy") || condition.Contains("partly"))
            return "⛅";
        else if (condition.Contains("cloudy") || condition.Contains("overcast"))
            return "☁️";
        else if (condition.Contains("rain") || condition.Contains("drizzle"))
            return "🌧️";
        else if (condition.Contains("snow"))
            return "❄️";
        else if (condition.Contains("thunder") || condition.Contains("storm"))
            return "⛈️";
        else if (condition.Contains("fog") || condition.Contains("mist"))
            return "🌫️";
        else if (condition.Contains("wind"))
            return "💨";
        else
            return "🌤️"; // Default icon
    }
}
