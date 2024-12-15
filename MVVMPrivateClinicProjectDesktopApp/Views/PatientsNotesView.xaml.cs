using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PatientsNotesView : UserControl {
    public PatientsNotesView(){
        InitializeComponent();
    }
    
    private void ShowPatientNoteDetails_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PatientsNotesViewModel viewModel) return;
        if (sender is not Button { DataContext: PatientNoteDto patientNoteDto }) return;
        
        viewModel.SetPatientNoteIdToShowDetails(patientNoteDto.Id);
        viewModel.ShowPatientNoteDetailsCommand.Execute(null);
    }
    
    private void ShowSelectPatient_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PatientsNotesViewModel viewModel) return;
        
        viewModel.SetDataInAddSpecificDataToPatientStore();
        viewModel.ShowSelectPatientToAddSpecificDataModal.Execute(null);
    }
}