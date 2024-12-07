using System.Windows;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewDoctorView : WindowViewBase {
    public AddNewDoctorView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        FirstNameInput.FormText = "";
        LastNameInput.FormText = "";
        PhoneNumberInput.FormText = "";
        DoctorSpecializationSelector = null;
        DoctorSpecializationInput.FormText = "";
    }
}