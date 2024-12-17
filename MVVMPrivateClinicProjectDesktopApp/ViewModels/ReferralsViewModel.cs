using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralsViewModel : DisplayEntitiesViewModelBase<ReferralDto> {
    private readonly ReferralStore _referralStore;
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    
    private ICommand LoadReferralsCommand { get; set; }
    public ICommand ShowReferralDetailsCommand { get; set; }
    public ICommand ShowSelectPatientToAddSpecificDataModal { get; set; }

    private ReferralsViewModel(ReferralStore referralStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending, SortingOptions.DateAscending, SortingOptions.DateDescending]){
        _referralStore = referralStore;
        _addSpecificDataToPatientStore = addSpecificDataToPatientStore;
        
        LoadReferralsCommand = new LoadReferralsDtoCommand(this, referralStore);
        ShowReferralDetailsCommand = modalNavigationViewModel.ShowReferralDetailsModal;
        ShowSelectPatientToAddSpecificDataModal = modalNavigationViewModel.ShowSelectPatientToAddSpecificDataModal;

        referralStore.ReferralCreated += OnReferralCreated;
    }

    public static ReferralsViewModel LoadReferralsViewModel(ReferralStore referralStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var referralsViewModel = new ReferralsViewModel(referralStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        referralsViewModel.LoadReferralsCommand.Execute(null);
        
        return referralsViewModel;
    }
    
    public void SetDataInAddSpecificDataToPatientStore(){
        _addSpecificDataToPatientStore.DataToAddName = "Referral";
        _addSpecificDataToPatientStore.DataColor =
            (SolidColorBrush) Application.Current.Resources["CustomYellowColor1"]!;
    }
    
    private void OnReferralCreated(ReferralDto referralDto){
        Entities.Add(referralDto);
    }
    
    public void SetReferralIdToShowDetails(int referralId){
        _referralStore.SelectedReferralId = referralId;
    }

    public override void UpdateEntities(IEnumerable<ReferralDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
    }

    protected override void SortEntities(){
        if (SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending) {
            ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption, nameof(ReferralDto.Id));
        }

        if (SelectedSortingOption is SortingOptions.AlphabeticalAscending or SortingOptions.AlphabeticalDescending) {
            ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
                nameof(ReferralDto.Name));
        }

        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
            nameof(ReferralDto.DateIssued));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not ReferralDto referralDto) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return referralDto.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               referralDto.PatientCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               referralDto.DoctorCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}