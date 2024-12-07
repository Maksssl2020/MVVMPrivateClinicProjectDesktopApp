using System.Windows;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewDiseaseView : WindowViewBase {
    public AddNewDiseaseView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        DiseaseNameInput.FormText = "";
    }
}