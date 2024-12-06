using System.Globalization;
using System.Windows.Data;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class PriceFormatConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is decimal price) {
            return $"{price:C}";
        }
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        throw new NotImplementedException();
    }
}