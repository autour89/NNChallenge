using Foundation;
using UIKit;

namespace NNChallenge.iOS.Views
{
    public class LocationPickerModel : UIPickerViewModel
    {
        private readonly string[] _locations;

        public LocationPickerModel(string[] locations)
        {
            _locations = locations;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _locations.Length;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _locations[row];
        }
    }
}
