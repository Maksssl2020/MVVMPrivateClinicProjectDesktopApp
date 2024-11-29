using System.Globalization;
using System.Windows.Data;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class DateTimeFormatConverter : IValueConverter {
    private const string Format = "dd-MM-yy, HH:mm";
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        return value is not DateTime dateTime ? value : dateTime.ToString(Format, culture);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        throw new NotImplementedException();
    }
}