using System.Globalization;
using System.Windows.Data;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class InvoiceStatusConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is InvoiceStatus invoiceStatus) {
            return invoiceStatus.Equals(InvoiceStatus.AwaitingPayment) ? "Awaiting Payment" : invoiceStatus.ToString();
        }
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        throw new NotImplementedException();
    }
}