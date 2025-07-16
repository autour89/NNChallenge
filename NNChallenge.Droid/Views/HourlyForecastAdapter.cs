using AndroidX.RecyclerView.Widget;
using Android.Views;
using Android.Widget;
using NNChallenge.Models;

namespace NNChallenge.Droid.Views;

public class HourlyForecastAdapter : RecyclerView.Adapter
{
    private List<HourlyForecastItem> _items;

    public HourlyForecastAdapter(List<HourlyForecastItem> items)
    {
        _items = items;
    }

    public override int ItemCount => _items.Count;

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var view = LayoutInflater.From(parent.Context)!.Inflate(Resource.Layout.hourly_forecast_item, parent, false);
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
    private readonly TextView _dateTimeText;
    private readonly TextView _temperatureText;
    private readonly TextView _conditionText;
    private readonly View _itemContainer;

    public HourlyForecastViewHolder(View itemView) : base(itemView)
    {
        _itemContainer = itemView.FindViewById(Resource.Id.item_container)!;
        _dateTimeText = itemView.FindViewById<TextView>(Resource.Id.date_time_text)!;
        _temperatureText = itemView.FindViewById<TextView>(Resource.Id.temperature_text)!;
        _conditionText = itemView.FindViewById<TextView>(Resource.Id.condition_text)!;
    }

    public void Bind(HourlyForecastItem item)
    {
        _dateTimeText.Text = item.FormattedDateTime;
        _temperatureText.Text = $"{item.TempC:F1}°C";
        _conditionText.Text = item.ConditionText;
    }
}
