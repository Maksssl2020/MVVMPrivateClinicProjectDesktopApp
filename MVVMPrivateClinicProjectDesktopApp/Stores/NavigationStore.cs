using System.Windows;
using System.Windows.Media;
using FontAwesome.Sharp;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class NavigationStore : NavigationStoreBase {
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
            default: {
                ViewIcon = IconChar.House;
                HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomLavenderColor1"]!;
                break;
            }
        }
    }
}