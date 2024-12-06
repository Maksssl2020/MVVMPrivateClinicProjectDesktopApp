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
    
    private PrescriptionsViewModel(PrescriptionStore prescriptionStore){
        _prescriptionStore = prescriptionStore;
        _prescriptions = [];
        
        LoadPrescriptionsCommand = new LoadPrescriptionsDtoCommand(this, _prescriptionStore);
        
        PrescriptionsView = CollectionViewSource.GetDefaultView(_prescriptions);
    }

    public static PrescriptionsViewModel LoadPrescriptionsViewModel(PrescriptionStore prescriptionStore){
        var prescriptionsViewModel = new PrescriptionsViewModel(prescriptionStore);
        
        prescriptionsViewModel.LoadPrescriptionsCommand.Execute(null);
        
        return prescriptionsViewModel;
    }
    
    public void UpdatePrescriptions(IEnumerable<PrescriptionDto> prescriptions){
        _prescriptions.Clear();

        foreach (var prescription in prescriptions) {
            _prescriptions.Add(prescription);
        }
    }
}