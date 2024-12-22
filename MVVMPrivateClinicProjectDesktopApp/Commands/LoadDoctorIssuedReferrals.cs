using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorIssuedReferrals(DoctorDetailsViewModel viewModel, ReferralStore referralStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            referralStore.SelectedDoctorId = viewModel.SelectedDoctorId;
            await referralStore.LoadDoctorIssuedReferrals();
            viewModel.UpdateIssuedReferrals(referralStore.SelectedDoctorIssuedReferrals);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}