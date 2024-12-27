using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PrescriptionsViewModel : DisplayEntitiesViewModelBase<PrescriptionDto, PrescriptionDetailsDto> {
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    
    public ICommand ShowPrescriptionDetailsModal { get; set; }
    public ICommand ShowSelectPatientToAddSpecificDataModal { get; set; }
    public ICommand GeneratePrescriptionPdfCommand { get; set; }
    
    private PrescriptionsViewModel(PrescriptionStore prescriptionStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel)
        : base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.DateAscending, SortingOptions.DateDescending],
            prescriptionStore,
            modalNavigationViewModel) {
        _addSpecificDataToPatientStore = addSpecificDataToPatientStore;
        
        ShowPrescriptionDetailsModal = modalNavigationViewModel.ShowPrescriptionDetailsModal;
        ShowSelectPatientToAddSpecificDataModal = modalNavigationViewModel.ShowSelectPatientToAddSpecificDataModal;
        GeneratePrescriptionPdfCommand = new GeneratePrescriptionPdfCommand(prescriptionStore);
    }

    public static PrescriptionsViewModel LoadPrescriptionsViewModel(PrescriptionStore prescriptionStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var prescriptionsViewModel = new PrescriptionsViewModel(prescriptionStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        prescriptionsViewModel.LoadEntitiesCommand.Execute(null);
        
        return prescriptionsViewModel;
    }

    public void GeneratePrescriptionPdf(int prescriptionId){
        GeneratePrescriptionPdfCommand.Execute(prescriptionId);
    }
    
    public void SetDataInAddSpecificDataToPatientStore(){
        _addSpecificDataToPatientStore.DataToAddName = "Prescription";
        _addSpecificDataToPatientStore.DataColor =
            (SolidColorBrush) Application.Current.Resources["CustomOrangeColor1"]!;
    }

    protected override void UpdateEntities(IEnumerable<PrescriptionDto> entities){
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