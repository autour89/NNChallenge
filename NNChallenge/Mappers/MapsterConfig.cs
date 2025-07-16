using Mapster;
using NNChallenge.Models;
using NNChallenge.Models.DAO;

namespace NNChallenge.Mappers;

public static class MapsterConfig
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<WeatherResponse, WeatherDataDAO>
            .NewConfig()
            .Map(dest => dest.Location, src => src.Location)
            .Map(dest => dest.Current, src => src.Current)
            .Map(dest => dest.Forecast, src => src.Forecast);

        TypeAdapterConfig<Location, LocationDAO>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Region, src => src.Region)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.Latitude, src => src.Lat)
            .Map(dest => dest.Longitude, src => src.Lon)
            .Map(dest => dest.TimeZoneId, src => src.TzId)
            .Map(dest => dest.LocalTime, src => ParseDateTime(src.Localtime));

        TypeAdapterConfig<Current, CurrentWeatherDAO>
            .NewConfig()
            .Map(dest => dest.LastUpdated, src => ParseDateTime(src.LastUpdated))
            .Map(dest => dest.TempC, src => src.TempC)
            .Map(dest => dest.TempF, src => src.TempF)
            .Map(dest => dest.IsDay, src => src.IsDay == 1)
            .Map(dest => dest.Condition, src => src.Condition)
            .Map(dest => dest.WindMph, src => src.WindMph)
            .Map(dest => dest.WindKph, src => src.WindKph)
            .Map(dest => dest.WindDegree, src => src.WindDegree)
            .Map(dest => dest.WindDirection, src => src.WindDir)
            .Map(dest => dest.PressureMb, src => src.PressureMb)
            .Map(dest => dest.PressureIn, src => src.PressureIn)
            .Map(dest => dest.PrecipMm, src => src.PrecipMm)
            .Map(dest => dest.PrecipIn, src => src.PrecipIn)
            .Map(dest => dest.Humidity, src => src.Humidity)
            .Map(dest => dest.Cloud, src => src.Cloud)
            .Map(dest => dest.FeelslikeC, src => src.FeelslikeC)
            .Map(dest => dest.FeelslikeF, src => src.FeelslikeF)
            .Map(dest => dest.VisKm, src => src.VisKm)
            .Map(dest => dest.VisMiles, src => src.VisMiles)
            .Map(dest => dest.UV, src => src.Uv)
            .Map(dest => dest.GustMph, src => src.GustMph)
            .Map(dest => dest.GustKph, src => src.GustKph);

        TypeAdapterConfig<Condition, WeatherConditionDAO>
            .NewConfig()
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.Icon, src => src.Icon)
            .Map(dest => dest.Code, src => src.Code);

        TypeAdapterConfig<Forecast, ForecastDAO>
            .NewConfig()
            .Map(dest => dest.ForecastDays, src => src.ForecastDay);

        TypeAdapterConfig<ForecastDay, ForecastDayDAO>
            .NewConfig()
            .Map(dest => dest.Date, src => ParseDate(src.Date))
            .Map(dest => dest.Day, src => src.Day)
            .Map(dest => dest.Astro, src => src.Astro)
            .Map(dest => dest.Hours, src => src.Hour);

        TypeAdapterConfig<Day, DayWeatherDAO>
            .NewConfig()
            .Map(dest => dest.MaxTempC, src => src.MaxtempC)
            .Map(dest => dest.MaxTempF, src => src.MaxtempF)
            .Map(dest => dest.MinTempC, src => src.MintempC)
            .Map(dest => dest.MinTempF, src => src.MintempF)
            .Map(dest => dest.AvgTempC, src => src.AvgtempC)
            .Map(dest => dest.AvgTempF, src => src.AvgtempF)
            .Map(dest => dest.MaxWindMph, src => src.MaxwindMph)
            .Map(dest => dest.MaxWindKph, src => src.MaxwindKph)
            .Map(dest => dest.TotalPrecipMm, src => src.TotalprecipMm)
            .Map(dest => dest.TotalPrecipIn, src => src.TotalprecipIn)
            .Map(dest => dest.TotalSnowCm, src => src.TotalsnowCm)
            .Map(dest => dest.AvgVisKm, src => src.AvgvisKm)
            .Map(dest => dest.AvgVisMiles, src => src.AvgvisMiles)
            .Map(dest => dest.AvgHumidity, src => src.Avghumidity)
            .Map(dest => dest.DailyWillItRain, src => src.DailyWillItRain == 1)
            .Map(dest => dest.DailyChanceOfRain, src => src.DailyChanceOfRain)
            .Map(dest => dest.DailyWillItSnow, src => src.DailyWillItSnow == 1)
            .Map(dest => dest.DailyChanceOfSnow, src => src.DailyChanceOfSnow)
            .Map(dest => dest.Condition, src => src.Condition)
            .Map(dest => dest.UV, src => src.Uv);

        TypeAdapterConfig<Astro, AstroDAO>
            .NewConfig()
            .Map(dest => dest.Sunrise, src => ParseTimeSpan(src.Sunrise))
            .Map(dest => dest.Sunset, src => ParseTimeSpan(src.Sunset))
            .Map(dest => dest.Moonrise, src => ParseTimeSpan(src.Moonrise))
            .Map(dest => dest.Moonset, src => ParseTimeSpan(src.Moonset))
            .Map(dest => dest.MoonPhase, src => src.MoonPhase)
            .Map(dest => dest.MoonIllumination, src => src.MoonIllumination);

        TypeAdapterConfig<Hour, HourlyWeatherDAO>
            .NewConfig()
            .Map(dest => dest.Time, src => ParseDateTime(src.Time))
            .Map(dest => dest.TempC, src => src.TempC)
            .Map(dest => dest.TempF, src => src.TempF)
            .Map(dest => dest.IsDay, src => src.IsDay == 1)
            .Map(dest => dest.Condition, src => src.Condition)
            .Map(dest => dest.WindMph, src => src.WindMph)
            .Map(dest => dest.WindKph, src => src.WindKph)
            .Map(dest => dest.WindDegree, src => src.WindDegree)
            .Map(dest => dest.WindDirection, src => src.WindDir)
            .Map(dest => dest.PressureMb, src => src.PressureMb)
            .Map(dest => dest.PressureIn, src => src.PressureIn)
            .Map(dest => dest.PrecipMm, src => src.PrecipMm)
            .Map(dest => dest.PrecipIn, src => src.PrecipIn)
            .Map(dest => dest.Humidity, src => src.Humidity)
            .Map(dest => dest.Cloud, src => src.Cloud)
            .Map(dest => dest.FeelslikeC, src => src.FeelslikeC)
            .Map(dest => dest.FeelslikeF, src => src.FeelslikeF)
            .Map(dest => dest.WindchillC, src => src.WindchillC)
            .Map(dest => dest.WindchillF, src => src.WindchillF)
            .Map(dest => dest.HeatIndexC, src => src.HeatindexC)
            .Map(dest => dest.HeatIndexF, src => src.HeatindexF)
            .Map(dest => dest.DewpointC, src => src.DewpointC)
            .Map(dest => dest.DewpointF, src => src.DewpointF)
            .Map(dest => dest.VisKm, src => src.VisKm)
            .Map(dest => dest.VisMiles, src => src.VisMiles)
            .Map(dest => dest.UV, src => src.Uv)
            .Map(dest => dest.GustMph, src => src.GustMph)
            .Map(dest => dest.GustKph, src => src.GustKph);
    }

    private static DateTime ParseDateTime(string dateTimeString)
    {
        if (DateTime.TryParse(dateTimeString, out var result))
            return result;
        return DateTime.MinValue;
    }

    private static DateTime ParseDate(string dateString)
    {
        if (
            DateTime.TryParseExact(
                dateString,
                "yyyy-MM-dd",
                null,
                System.Globalization.DateTimeStyles.None,
                out var result
            )
        )
            return result;
        return DateTime.MinValue;
    }

    private static TimeSpan ParseTimeSpan(string timeString)
    {
        if (TimeSpan.TryParse(timeString, out var result))
            return result;
        return TimeSpan.Zero;
    }
}
