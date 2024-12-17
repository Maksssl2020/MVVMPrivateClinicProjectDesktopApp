using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PrescriptionsViewModel : DisplayEntitiesViewModelBase<PrescriptionDto> {
    private readonly PrescriptionStore _prescriptionStore;
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    
    private ICommand LoadPrescriptionsCommand { get; set; }
    public ICommand ShowPrescriptionDetailsModal { get; set; }
    public ICommand ShowSelectPatientToAddSpecificDataModal { get; set; }
    
    private PrescriptionsViewModel(PrescriptionStore prescriptionStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel)
        : base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.DateAscending, SortingOptions.DateDescending]) {
        _prescriptionStore = prescriptionStore;
        _addSpecificDataToPatientStore = addSpecificDataToPatientStore;
        
        LoadPrescriptionsCommand = new LoadPrescriptionsDtoCommand(this, prescriptionStore);
        ShowPrescriptionDetailsModal = modalNavigationViewModel.ShowPrescriptionDetailsModal;
        ShowSelectPatientToAddSpecificDataModal = modalNavigationViewModel.ShowSelectPatientToAddSpecificDataModal;
        
        prescriptionStore.PrescriptionCreated += OnPrescriptionCreated;
    }

    public static PrescriptionsViewModel LoadPrescriptionsViewModel(PrescriptionStore prescriptionStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var prescriptionsViewModel = new PrescriptionsViewModel(prescriptionStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        prescriptionsViewModel.LoadPrescriptionsCommand.Execute(null);
        
        return prescriptionsViewModel;
    }

    private void OnPrescriptionCreated(PrescriptionDto prescription){
        Entities.Add(prescription);
    }

    public void SetPrescriptionIdToSeeDetails(int prescriptionId){
        _prescriptionStore.SelectedPrescriptionId = prescriptionId;
    }

    public void SetDataInAddSpecificDataToPatientStore(){
        _addSpecificDataToPatientStore.DataToAddName = "Prescription";
        _addSpecificDataToPatientStore.DataColor =
            (SolidColorBrush) Application.Current.Resources["CustomOrangeColor1"]!;
    }
    
    public override void UpdateEntities(IEnumerable<PrescriptionDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
        
        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
            SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending
                ? nameof(PrescriptionDto.Id)
                : nameof(PrescriptionDto.DateIssued));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not PrescriptionDto prescriptionDto) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return prescriptionDto.PatientCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               prescriptionDto.DoctorCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}