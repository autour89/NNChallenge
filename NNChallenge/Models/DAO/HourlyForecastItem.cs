namespace NNChallenge.Models.DAO;

public class HourlyForecastItem
{
    public DateTime Time { get; set; }
    public double TempC { get; set; }
    public double TempF { get; set; }
    public string ConditionText { get; set; } = string.Empty;
    public int Humidity { get; set; }
    public bool IsDay { get; set; }

    public string FormattedTime => Time.ToString("HH:mm");
    public string FormattedDate => Time.ToString("MMM dd, yyyy");
}
