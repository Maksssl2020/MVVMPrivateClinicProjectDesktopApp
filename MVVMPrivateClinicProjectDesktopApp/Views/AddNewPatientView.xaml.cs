using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewPatientView : WindowViewBase {
    public AddNewPatientView() {
        InitializeComponent();
    }

    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        FirstNameInput.FormText = "";
        LastNameInput.FormText = "";
        PhoneNumberInput.FormText = "";
        EmailInput.FormText = "";
        CityInput.FormText = "";
        PostalCodeInput.FormText = "";
        StreetInput.FormText = "";
        BuildingNumberInput.FormText = "";
        LocalNumberInput.FormText = "";
    }
}