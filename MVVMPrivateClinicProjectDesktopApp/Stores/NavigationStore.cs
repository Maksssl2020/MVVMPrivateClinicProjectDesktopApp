using System.Windows;
using System.Windows.Media;
using FontAwesome.Sharp;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class NavigationStore : NavigationStoreBase<ViewModelBase> {
    public string ViewTitle { get; private set; } = null!;
    public IconChar ViewIcon { get; private set; }
    public SolidColorBrush HeaderBrush { get; private set; } = null!;

    public void ChangeCurrentViewModel(ViewModelBase viewModel, string viewTitle){
        CurrentViewModel?.Dispose();

        CurrentViewModel = viewModel;
        ViewTitle = viewTitle;
        ChangeIconAndColorInHeaderDependsOnViewModelTitle(viewTitle);
        
        OnCurrentViewModelChanged();
    }

    private void ChangeIconAndColorInHeaderDependsOnViewModelTitle(string viewTitle) {
        switch (viewTitle) {
            case "Home": {
                ViewIcon = IconChar.House;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomLavenderColor1"]!;
                break;
            }
            case "Patients": {
                ViewIcon = IconChar.UserInjured;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomGreenColor1"]!;
                break;
            }
            case "Doctors": {
                ViewIcon = IconChar.UserDoctor;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomVioletColor1"]!;
                break;
            }
            case "Appointments": {
                ViewIcon = IconChar.Calendar;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomRedColor1"]!;
                break;
            }
            case "Diseases": {
                ViewIcon = IconChar.Disease;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomTurquoiseColor1"]!;
                break;
            }
            case "Medicines": {
                ViewIcon = IconChar.Capsules;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomPlumColor1"]!;
                break;
            }
            case "Prescriptions": {
             ViewIcon = IconChar.PrescriptionBottleMedical;
             HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomOrangeColor1"]!;
             break;
            }
            case "Referrals": {
                ViewIcon = IconChar.Receipt;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomYellowColor1"]!;
                break;
            }
            case "Invoices": {
                ViewIcon = IconChar.FileInvoiceDollar;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomCoralColor1"]!;
                break;
            }
            case "Pricing": {
                ViewIcon = IconChar.MoneyBills;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomPaleGoldColor1"]!;
                break;
            }
            case "Referral Tests": {
                ViewIcon = IconChar.Microscope;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomWarmBeigeColor1"]!;
                break;
            }
            case "Disabled Data": {
                ViewIcon = IconChar.Trash;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomSlateGrayColor1"]!;
                break;
            }
            default: {
                ViewIcon = IconChar.House;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomLavenderColor1"]!;
                break;
            }
        }
    }
}