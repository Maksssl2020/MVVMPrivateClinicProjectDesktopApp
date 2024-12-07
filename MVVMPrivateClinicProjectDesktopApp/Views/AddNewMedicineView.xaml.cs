using System.Windows;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewMedicineView : WindowViewBase {
    public AddNewMedicineView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        MedicineNameInput.FormText = "";
        MedicineDescriptionInput.FormText = "";
        MedicineTypeInput.FormText = "";
        MedicineTypeSelector.SelectedItem = null;
    }
}