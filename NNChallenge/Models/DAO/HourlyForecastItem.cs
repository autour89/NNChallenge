namespace NNChallenge.Models.DAO;

public class HourlyForecastItem
{
    public DateTime Time { get; set; }
    public double TempC { get; set; }
    public double TempF { get; set; }
    public string ConditionText { get; set; } = string.Empty;
    public string ConditionIcon { get; set; } = string.Empty;
    public int Humidity { get; set; }
    public double FeelslikeC { get; set; }
    public double FeelslikeF { get; set; }
    public double WindKph { get; set; }
    public double WindMph { get; set; }
    public string WindDirection { get; set; } = string.Empty;
    public double PrecipMm { get; set; }
    public int Cloud { get; set; }
    public bool IsDay { get; set; }

    public string FormattedTime => Time.ToString("HH:mm");
    public string FormattedDate => Time.ToString("MMM dd, yyyy");
    public string FormattedDateTime => Time.ToString("MMM dd, yyyy - HH:mm");

    public DateTime DateTime => Time;
    public string WindDir => WindDirection;
}
