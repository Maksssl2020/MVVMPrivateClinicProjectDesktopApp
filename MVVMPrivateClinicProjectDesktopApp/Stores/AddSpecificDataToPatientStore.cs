using System.Windows.Media;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class AddSpecificDataToPatientStore {
    public string DataToAddName { get; set; } = null!;
    public SolidColorBrush DataColor { get; set; } = null!;
}