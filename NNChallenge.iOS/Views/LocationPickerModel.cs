namespace NNChallenge.iOS.Views;

public class LocationPickerModel(string[] locations) : UIPickerViewModel
{
    private readonly string[] _locations = locations;

    public override nint GetComponentCount(UIPickerView pickerView) => 1;

    public override nint GetRowsInComponent(UIPickerView pickerView, nint component) =>
        _locations.Length;

    public override string GetTitle(UIPickerView pickerView, nint row, nint component) =>
        _locations[row];
}
