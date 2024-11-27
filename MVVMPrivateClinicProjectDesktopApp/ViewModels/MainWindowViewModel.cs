using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.Sharp;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Services;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MainWindowViewModel: ViewModelBase {
    private readonly NavigationStore _navigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    public string ViewTitle => _navigationStore.ViewTitle;
    public IconChar ViewIcon => _navigationStore.ViewIcon;
    public SolidColorBrush HeaderBrush => _navigationStore.HeaderBrush;

    public NavigationBarViewModel NavigationBarViewModel { get; }

    public MainWindowViewModel(NavigationStore navigationStore, NavigationBarViewModel navigationBarViewModel){
        _navigationStore = navigationStore;
        NavigationBarViewModel = navigationBarViewModel;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        Console.WriteLine(ViewTitle);
    }

    public MainWindowViewModel() {}
    
    private void OnCurrentViewModelChanged(){
        OnPropertyChanged(nameof(CurrentViewModel));
        OnPropertyChanged(nameof(ViewTitle));
        OnPropertyChanged(nameof(ViewIcon));
        OnPropertyChanged(nameof(HeaderBrush));
    }
}