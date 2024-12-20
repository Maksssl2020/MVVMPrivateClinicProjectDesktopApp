using System.Globalization;
using System.Windows.Data;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class WidthSearchBarAdjustmentConverter : IValueConverter {
    private const double IconWidth = 20d;
    private const double Padding = 10d;
        
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is double actualWidth) {
            return actualWidth - IconWidth - Padding;
        }
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        throw new NotImplementedException();
    }
}