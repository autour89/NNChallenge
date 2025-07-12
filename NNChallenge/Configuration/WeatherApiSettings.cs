namespace NNChallenge.Configuration;

public class WeatherApiSettings
{
    public string BaseUrl { get; set; } = "https://api.weatherapi.com";
    public string ApiKey { get; set; } = "898147f83a734b7dbaa95705211612";
    public int DefaultForecastDays { get; set; } = 3;
    public int RequestTimeoutSeconds { get; set; } = 30;
}
