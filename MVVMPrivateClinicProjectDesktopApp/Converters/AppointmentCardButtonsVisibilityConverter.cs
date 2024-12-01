using System.Globalization;
using System.Windows;
using System.Windows.Data;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Converters;

public class AppointmentCardButtonsVisibilityConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){
        if (value is not string status) return Visibility.Collapsed;
        var result = TryParse(status, out AppointmentStatus appointmentStatus);
            
        return result ? appointmentStatus == AppointmentStatus.Scheduled ? Visibility.Visible : Visibility.Collapsed : Visibility.Collapsed;

    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture){
        throw new NotImplementedException();
    }
}