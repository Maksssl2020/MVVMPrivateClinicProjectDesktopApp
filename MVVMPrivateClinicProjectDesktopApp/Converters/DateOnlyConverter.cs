using System.Globalization;
using System.Windows.Data;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class DateOnlyConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is DateOnly dateOnly) {
            return dateOnly.ToString("dd.MM.yyyy (dddd)", culture);
        }
        
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is string dateString && DateOnly.TryParseExact(dateString, "dd.MM.yyyy (dddd)", culture, DateTimeStyles.None, out var dateOnly)) {
            return dateOnly;
        }
        
        return null;
    }
}