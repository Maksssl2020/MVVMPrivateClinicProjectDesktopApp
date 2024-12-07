using System.Globalization;
using System.Windows.Data;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class DecimalConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is decimal decimalValue) {
            return decimalValue.ToString("0.##", culture);
        }
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is not string stringValue) return Binding.DoNothing;
        
        return decimal.TryParse(stringValue, NumberStyles.Number, culture, out var decimalValue) ? 
            decimalValue : Binding.DoNothing;
    }
}