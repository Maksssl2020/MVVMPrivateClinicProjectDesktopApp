using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PrescriptionsViewModel : ViewModelBase {
    private readonly PrescriptionStore _prescriptionStore;
    
    private readonly ObservableCollection<PrescriptionDto>  _prescriptions;
    public ICollectionView PrescriptionsView { get; set; }

    private ICommand LoadPrescriptionsCommand { get; set; }
    public ICommand ShowPrescriptionDetailsModal { get; set; }
    
    private PrescriptionsViewModel(PrescriptionStore prescriptionStore, ModalNavigationViewModel modalNavigationViewModel){
        _prescriptionStore = prescriptionStore;
        _prescriptions = [];
        
        LoadPrescriptionsCommand = new LoadPrescriptionsDtoCommand(this, prescriptionStore);
        ShowPrescriptionDetailsModal = modalNavigationViewModel.ShowPrescriptionDetailsModal;
        
        PrescriptionsView = CollectionViewSource.GetDefaultView(_prescriptions);
        prescriptionStore.PrescriptionCreated += OnPrescriptionCreated;
    }

    public static PrescriptionsViewModel LoadPrescriptionsViewModel(PrescriptionStore prescriptionStore, ModalNavigationViewModel modalNavigationViewModel){
        var prescriptionsViewModel = new PrescriptionsViewModel(prescriptionStore, modalNavigationViewModel);
        
        prescriptionsViewModel.LoadPrescriptionsCommand.Execute(null);
        
        return prescriptionsViewModel;
    }

    private void OnPrescriptionCreated(PrescriptionDto prescription){
        _prescriptions.Add(prescription);
    }

    public void SetPrescriptionIdToSeeDetails(int prescriptionId){
        _prescriptionStore.SelectedPrescriptionId = prescriptionId;
    }
    
    public void UpdatePrescriptions(IEnumerable<PrescriptionDto> prescriptions){
        _prescriptions.Clear();

        foreach (var prescription in prescriptions) {
            _prescriptions.Add(prescription);
        }
    }
}