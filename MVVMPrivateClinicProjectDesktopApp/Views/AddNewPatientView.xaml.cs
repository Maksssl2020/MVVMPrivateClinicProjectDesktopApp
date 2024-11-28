using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewPatientView : Window {
    public AddNewPatientView(){
        InitializeComponent();
        ResizeMode = ResizeMode.NoResize;
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
    
    private void buttonClose_Click(object sender, RoutedEventArgs e) => Close();
    private void MouseLeftButtonDown_Hold(object sender, MouseButtonEventArgs e) => DragMove();
}