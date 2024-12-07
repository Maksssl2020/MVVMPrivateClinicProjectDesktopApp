using System.Globalization;
using System.Windows.Data;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class DateOnlyConverter : IValueConverter {
    private const string Format = "dd-MM-yy";
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        return value is not DateOnly dateOnly ? value : dateOnly.ToString(Format, culture);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        throw new NotImplementedException();
    }
}