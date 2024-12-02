using System.Windows.Media;
using FontAwesome.Sharp;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MainWindowViewModel: ViewModelBase {
    private readonly NavigationStore _navigationStore;
    public NavigationBarViewModel NavigationBarViewModel { get; }
    
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel!;
    public string ViewTitle => _navigationStore.ViewTitle;
    public IconChar ViewIcon => _navigationStore.ViewIcon;
    public SolidColorBrush HeaderBrush => _navigationStore.HeaderBrush;


    public MainWindowViewModel(NavigationStore navigationStore, NavigationBarViewModel navigationBarViewModel){
        _navigationStore = navigationStore;
        NavigationBarViewModel = navigationBarViewModel;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged(){
        OnPropertyChanged(nameof(CurrentViewModel));
        OnPropertyChanged(nameof(ViewTitle));
        OnPropertyChanged(nameof(ViewIcon));
        OnPropertyChanged(nameof(HeaderBrush));
    }
}