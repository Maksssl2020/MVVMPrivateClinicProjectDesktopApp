using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PrescriptionsViewModel : ViewModelBase {
    private readonly PrescriptionStore _prescriptionStore;
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    
    private readonly ObservableCollection<PrescriptionDto>  _prescriptions;
    public ICollectionView PrescriptionsView { get; set; }

    private ICommand LoadPrescriptionsCommand { get; set; }
    public ICommand ShowPrescriptionDetailsModal { get; set; }
    public ICommand ShowSelectPatientToAddSpecificDataModal { get; set; }
    
    private PrescriptionsViewModel(PrescriptionStore prescriptionStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        _prescriptionStore = prescriptionStore;
        _addSpecificDataToPatientStore = addSpecificDataToPatientStore;
        _prescriptions = [];
        
        LoadPrescriptionsCommand = new LoadPrescriptionsDtoCommand(this, prescriptionStore);
        ShowPrescriptionDetailsModal = modalNavigationViewModel.ShowPrescriptionDetailsModal;
        ShowSelectPatientToAddSpecificDataModal = modalNavigationViewModel.ShowSelectPatientToAddSpecificDataModal;
        
        PrescriptionsView = CollectionViewSource.GetDefaultView(_prescriptions);
        prescriptionStore.PrescriptionCreated += OnPrescriptionCreated;
    }

    public static PrescriptionsViewModel LoadPrescriptionsViewModel(PrescriptionStore prescriptionStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var prescriptionsViewModel = new PrescriptionsViewModel(prescriptionStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        prescriptionsViewModel.LoadPrescriptionsCommand.Execute(null);
        
        return prescriptionsViewModel;
    }

    private void OnPrescriptionCreated(PrescriptionDto prescription){
        _prescriptions.Add(prescription);
    }

    public void SetPrescriptionIdToSeeDetails(int prescriptionId){
        _prescriptionStore.SelectedPrescriptionId = prescriptionId;
    }

    public void SetDataInAddSpecificDataToPatientStore(){
        _addSpecificDataToPatientStore.DataToAddName = "Prescription";
        _addSpecificDataToPatientStore.DataColor =
            (SolidColorBrush) Application.Current.Resources["CustomOrangeColor1"]!;
    }
    
    public void UpdatePrescriptions(IEnumerable<PrescriptionDto> prescriptions){
        _prescriptions.Clear();

        foreach (var prescription in prescriptions) {
            _prescriptions.Add(prescription);
        }
    }
}