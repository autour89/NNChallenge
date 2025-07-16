using Android.Views;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Models.DAO;

namespace NNChallenge.Droid.Views;

public class HourlyForecastAdapter(List<HourlyForecastItem> items) : RecyclerView.Adapter
{
    private List<HourlyForecastItem> _items = items;

    public override int ItemCount => _items.Count;

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var view = LayoutInflater
            .From(parent.Context)!
            .Inflate(Resource.Layout.hourly_forecast_item, parent, false);
        return new HourlyForecastViewHolder(view!);
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        var viewHolder = (HourlyForecastViewHolder)holder;
        viewHolder.Bind(_items[position]);
    }

    public void UpdateData(List<HourlyForecastItem> newItems)
    {
        _items = newItems;
        NotifyDataSetChanged();
    }
}

public class HourlyForecastViewHolder : RecyclerView.ViewHolder
{
    private readonly TextView _dateText;
    private readonly TextView _timeText;
    private readonly TextView _conditionText;
    private readonly TextView _humidityText;
    private readonly TextView _temperatureText;
    private readonly View _itemContainer;

    public HourlyForecastViewHolder(View itemView)
        : base(itemView)
    {
        _itemContainer = itemView.FindViewById(Resource.Id.item_container)!;
        _dateText = itemView.FindViewById<TextView>(Resource.Id.date_text)!;
        _timeText = itemView.FindViewById<TextView>(Resource.Id.time_text)!;
        _conditionText = itemView.FindViewById<TextView>(Resource.Id.condition_text)!;
        _humidityText = itemView.FindViewById<TextView>(Resource.Id.humidity_text)!;
        _temperatureText = itemView.FindViewById<TextView>(Resource.Id.temperature_text)!;
    }

    public void Bind(HourlyForecastItem item)
    {
        _dateText.Text = $"📅 {item.FormattedDate}";
        _timeText.Text = $"🕐 {item.FormattedTime}";

        var weatherIcon = GetWeatherIcon(item.ConditionText);
        _conditionText.Text = $"{weatherIcon} {item.ConditionText}";

        _humidityText.Text = $"💧 {item.Humidity}%";
        _temperatureText.Text = $"{item.TempC:F1}°C";
    }

    private static string GetWeatherIcon(string condition)
    {
        var lowerCondition = condition.ToLower();

        return lowerCondition switch
        {
            string c when c.Contains("sunny") || c.Contains("clear") => "☀️",
            string c when c.Contains("cloudy") || c.Contains("overcast") => "☁️",
            string c when c.Contains("partly") => "⛅",
            string c when c.Contains("rain") || c.Contains("drizzle") => "🌧️",
            string c when c.Contains("snow") => "❄️",
            string c when c.Contains("storm") || c.Contains("thunder") => "⛈️",
            string c when c.Contains("fog") || c.Contains("mist") => "🌫️",
            string c when c.Contains("wind") => "💨",
            _ => "🌤️",
        };
    }
}
