using System.Windows;
using System.Windows.Controls;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewPatientNoteView : UserControl {
    public AddNewPatientNoteView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        DoctorSelector.SelectedItem = null;
        NoteDescriptionInput.FormText = "";
    }
}