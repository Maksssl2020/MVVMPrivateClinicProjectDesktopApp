using System.Globalization;
using System.Windows.Data;
using MVVMPrivateClinicProjectDesktopApp.Helpers;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class SortingOptionDisplayConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        if
            (value is SortingOptions sortingOption) {
            return sortingOption switch {
                SortingOptions.AlphabeticalAscending => "Alphabetical Asc.",
                SortingOptions.AlphabeticalDescending => "Alphabetical Desc.",
                SortingOptions.IdAscending => "Id Asc.",
                SortingOptions.IdDescending => "Id Desc.",
                _ => "Unknown"
            };
        }
        
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        throw new NotImplementedException();
    }
}