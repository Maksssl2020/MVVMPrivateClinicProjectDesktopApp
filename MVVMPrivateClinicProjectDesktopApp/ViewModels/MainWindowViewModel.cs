using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.Sharp;
using Microsoft.Extensions.DependencyInjection;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Views;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MainWindowViewModel: ViewModelBase {
    private ViewModelBase _currentView = null!;
    private string _viewTitle = null!;
    private IconChar _viewIcon;
    private SolidColorBrush _headerBrush = null!;
    
    public ViewModelBase CurrentView {
        get => _currentView;
        set {_currentView = value; OnPropertyChanged();}
    }

    public string ViewTitle {
        get => _viewTitle; 
        set {_viewTitle = value; OnPropertyChanged();}
    }

    public IconChar ViewIcon {
        get => _viewIcon; 
        set {_viewIcon = value; OnPropertyChanged();}
    }

    public SolidColorBrush HeaderBrush {
        get => _headerBrush;
        set {_headerBrush = value; OnPropertyChanged();}
    }
    
    public ICommand ShowHomeViewCommand { get; set; }
    public ICommand ShowPatientsViewCommand { get; set; }
    public ICommand ShowDoctorsViewCommand { get; set; }
    public ICommand ShowDiseasesViewCommand { get; set; }
    public ICommand ShowMedicinesViewCommand { get; set; }


    public MainWindowViewModel(){
        ShowHomeViewCommand = new NavigateCommand<HomeViewModel>(ExecuteShowHomeViewCommand);
        ShowPatientsViewCommand = new NavigateCommand<PatientsViewModel>(ExecuteShowPatientsViewCommand);
        ShowDoctorsViewCommand = new NavigateCommand<DoctorsViewModel>(ExecuteShowDoctorsViewCommand);
        ShowDiseasesViewCommand = new NavigateCommand<DiseasesViewModel>(ExecuteShowDiseasesViewCommand);
        ShowMedicinesViewCommand = new NavigateCommand<MedicinesViewModel>(ExecuteShowMedicinesViewCommand);
        
        ExecuteShowHomeViewCommand();
    }

    private void ExecuteShowHomeViewCommand(){
        CurrentView = new HomeViewModel();
        ViewTitle = "Home";
        ViewIcon = IconChar.House;
        HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomLavenderColor1"]!;
    }
    
    private void ExecuteShowPatientsViewCommand(){
        CurrentView = new PatientsViewModel();
        ViewTitle = "Patients";
        ViewIcon = IconChar.UserInjured;
        HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomGreenColor1"]!;
    }

    private void ExecuteShowDoctorsViewCommand(){
        CurrentView = new DoctorsViewModel();
        ViewTitle = "Doctors";
        ViewIcon = IconChar.UserDoctor;
        HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomVioletColor1"]!;
    }
    
    private void ExecuteShowDiseasesViewCommand(){
        CurrentView = new DiseasesViewModel();
        ViewTitle = "Diseases";
        ViewIcon = IconChar.Disease;
        HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomTurquoiseColor1"]!;
    }
    
    private void ExecuteShowMedicinesViewCommand(){
        CurrentView = new MedicinesViewModel();
        ViewTitle = "Medicines";
        ViewIcon = IconChar.Capsules;
        HeaderBrush = (SolidColorBrush)Application.Current.Resources["CustomPlumColor1"]!;
    }
}