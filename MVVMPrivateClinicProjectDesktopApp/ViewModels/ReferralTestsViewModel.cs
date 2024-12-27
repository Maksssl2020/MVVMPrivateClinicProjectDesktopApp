using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralTestsViewModel : DisplayEntitiesViewModelBase<ReferralTestDto, ReferralTestDetailsDto> {
    public ICommand ShowAddNewReferralTestModalCommand { get; set; }
    public ICommand ShowReferralTestDetailsModalCommand { get; set; }

    private ReferralTestsViewModel(ReferralTestStore referralTestStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending],
            referralTestStore,
            modalNavigationViewModel) {
        ShowAddNewReferralTestModalCommand = modalNavigationViewModel.ShowAddNewReferralTestModal;
        ShowReferralTestDetailsModalCommand = modalNavigationViewModel.ShowReferralTestDetailsModal;
    }

    public static ReferralTestsViewModel LoadReferralTestsViewModel(ReferralTestStore referralTestStore, ModalNavigationViewModel modalNavigationViewModel){
        var referralTestsViewModel = new ReferralTestsViewModel(referralTestStore, modalNavigationViewModel);
        
        referralTestsViewModel.LoadEntitiesCommand.Execute(null);
        
        return referralTestsViewModel;
    }

    protected override void UpdateEntities(IEnumerable<ReferralTestDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }

        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
            SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending
                ? nameof(ReferralTestDto.Id)
                : nameof(ReferralTestDto.Name));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not ReferralTestDto referralTestDto) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return referralTestDto.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}