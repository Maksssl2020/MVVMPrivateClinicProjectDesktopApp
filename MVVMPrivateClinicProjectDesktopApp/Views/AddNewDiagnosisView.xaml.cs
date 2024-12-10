using System.Windows;
using System.Windows.Controls;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewDiagnosisView : UserControl {
    public AddNewDiagnosisView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        DiagnosisDescriptionInput.FormText = "";
        DoctorSelector.SelectedItem = null!;
        DiseaseSelector.SelectedItem = null!;
    }
}