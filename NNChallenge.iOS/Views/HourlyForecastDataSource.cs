using NNChallenge.Models.DAO;

namespace NNChallenge.iOS.Views;

public class HourlyForecastDataSource(List<HourlyForecastItem> items) : UICollectionViewDataSource
{
    private List<HourlyForecastItem> _items = items;

    public void UpdateData(List<HourlyForecastItem> newItems)
    {
        _items = newItems;
    }

    public override nint NumberOfSections(UICollectionView collectionView) => 1;

    public override nint GetItemsCount(UICollectionView collectionView, nint section) =>
        _items.Count;

    public override UICollectionViewCell GetCell(
        UICollectionView collectionView,
        NSIndexPath indexPath
    )
    {
        var cell =
            collectionView.DequeueReusableCell(HourlyForecastCell.CellIdentifier, indexPath)
            as HourlyForecastCell;

        cell ??= new HourlyForecastCell(IntPtr.Zero);

        if (indexPath.Row < _items.Count)
        {
            cell.ConfigureCell(_items[indexPath.Row]);
        }

        return cell;
    }
}

public class HourlyForecastDelegate : UICollectionViewDelegate
{
    public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
    {
        collectionView.DeselectItem(indexPath, true);
    }
}
