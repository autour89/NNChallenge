using System;

namespace NNChallenge.Models;

/// <summary>
/// Represents a single hourly forecast item combining date and hour data
/// </summary>
public class HourlyForecastItem
{
    public DateTime DateTime { get; set; }
    public string FormattedDateTime { get; set; } = string.Empty;
    public string FormattedTime { get; set; } = string.Empty;
    public string FormattedDate { get; set; } = string.Empty;
    public double TempC { get; set; }
    public double TempF { get; set; }
    public string ConditionText { get; set; } = string.Empty;
    public string ConditionIcon { get; set; } = string.Empty;
    public int Humidity { get; set; }
    public double FeelslikeC { get; set; }
    public double FeelslikeF { get; set; }
    public double WindKph { get; set; }
    public double WindMph { get; set; }
    public string WindDir { get; set; } = string.Empty;
    public double PrecipMm { get; set; }
    public int Cloud { get; set; }
    public bool IsDay { get; set; }

    public static HourlyForecastItem FromHour(Hour hour, string date)
    {
        var dateTime = DateTime.Parse(hour.Time);
        
        return new HourlyForecastItem
        {
            DateTime = dateTime,
            FormattedDateTime = dateTime.ToString("MMM dd, yyyy - HH:mm"),
            FormattedTime = dateTime.ToString("HH:mm"),
            FormattedDate = dateTime.ToString("MMM dd, yyyy"),
            TempC = hour.TempC,
            TempF = hour.TempF,
            ConditionText = hour.Condition.Text,
            ConditionIcon = hour.Condition.Icon,
            Humidity = hour.Humidity,
            FeelslikeC = hour.FeelslikeC,
            FeelslikeF = hour.FeelslikeF,
            WindKph = hour.WindKph,
            WindMph = hour.WindMph,
            WindDir = hour.WindDir,
            PrecipMm = hour.PrecipMm,
            Cloud = hour.Cloud,
            IsDay = hour.IsDay == 1
        };
    }
}
