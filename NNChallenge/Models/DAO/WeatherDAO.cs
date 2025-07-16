namespace NNChallenge.Models.DAO;

public class WeatherDataDAO
{
    public LocationDAO Location { get; set; } = new();
    public CurrentWeatherDAO Current { get; set; } = new();
    public ForecastDAO Forecast { get; set; } = new();
}

public class LocationDAO
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string TimeZoneId { get; set; } = string.Empty;
    public DateTime LocalTime { get; set; }
}

public class CurrentWeatherDAO
{
    public DateTime LastUpdated { get; set; }
    public double TempC { get; set; }
    public double TempF { get; set; }
    public bool IsDay { get; set; }
    public WeatherConditionDAO Condition { get; set; } = new();
    public double WindMph { get; set; }
    public double WindKph { get; set; }
    public int WindDegree { get; set; }
    public string WindDirection { get; set; } = string.Empty;
    public double PressureMb { get; set; }
    public double PressureIn { get; set; }
    public double PrecipMm { get; set; }
    public double PrecipIn { get; set; }
    public int Humidity { get; set; }
    public int Cloud { get; set; }
    public double FeelslikeC { get; set; }
    public double FeelslikeF { get; set; }
    public double VisKm { get; set; }
    public double VisMiles { get; set; }
    public double UV { get; set; }
    public double GustMph { get; set; }
    public double GustKph { get; set; }
}

public class WeatherConditionDAO
{
    public string Text { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int Code { get; set; }
}

public class ForecastDAO
{
    public List<ForecastDayDAO> ForecastDays { get; set; } = new();
}

public class ForecastDayDAO
{
    public DateTime Date { get; set; }
    public DayWeatherDAO Day { get; set; } = new();
    public AstroDAO Astro { get; set; } = new();
    public List<HourlyWeatherDAO> Hours { get; set; } = new();
}

public class DayWeatherDAO
{
    public double MaxTempC { get; set; }
    public double MaxTempF { get; set; }
    public double MinTempC { get; set; }
    public double MinTempF { get; set; }
    public double AvgTempC { get; set; }
    public double AvgTempF { get; set; }
    public double MaxWindMph { get; set; }
    public double MaxWindKph { get; set; }
    public double TotalPrecipMm { get; set; }
    public double TotalPrecipIn { get; set; }
    public double TotalSnowCm { get; set; }
    public double AvgVisKm { get; set; }
    public double AvgVisMiles { get; set; }
    public int AvgHumidity { get; set; }
    public bool DailyWillItRain { get; set; }
    public int DailyChanceOfRain { get; set; }
    public bool DailyWillItSnow { get; set; }
    public int DailyChanceOfSnow { get; set; }
    public WeatherConditionDAO Condition { get; set; } = new();
    public double UV { get; set; }
}

public class AstroDAO
{
    public TimeSpan Sunrise { get; set; }
    public TimeSpan Sunset { get; set; }
    public TimeSpan Moonrise { get; set; }
    public TimeSpan Moonset { get; set; }
    public string MoonPhase { get; set; } = string.Empty;
    public int MoonIllumination { get; set; }
}

public class HourlyWeatherDAO
{
    public DateTime Time { get; set; }
    public double TempC { get; set; }
    public double TempF { get; set; }
    public bool IsDay { get; set; }
    public WeatherConditionDAO Condition { get; set; } = new();
    public double WindMph { get; set; }
    public double WindKph { get; set; }
    public int WindDegree { get; set; }
    public string WindDirection { get; set; } = string.Empty;
    public double PressureMb { get; set; }
    public double PressureIn { get; set; }
    public double PrecipMm { get; set; }
    public double PrecipIn { get; set; }
    public int Humidity { get; set; }
    public int Cloud { get; set; }
    public double FeelslikeC { get; set; }
    public double FeelslikeF { get; set; }
    public double WindchillC { get; set; }
    public double WindchillF { get; set; }
    public double HeatIndexC { get; set; }
    public double HeatIndexF { get; set; }
    public double DewpointC { get; set; }
    public double DewpointF { get; set; }
    public double VisKm { get; set; }
    public double VisMiles { get; set; }
    public double UV { get; set; }
    public double GustMph { get; set; }
    public double GustKph { get; set; }
}
