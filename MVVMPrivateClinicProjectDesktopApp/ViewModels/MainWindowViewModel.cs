using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.Sharp;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using Brush = System.Drawing.Brush;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MainWindowViewModel : ViewModelBase {
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


    public MainWindowViewModel(){
        ShowHomeViewCommand = new NavigateCommand<HomeViewModel>(ExecuteShowHomeViewCommand);
        ShowPatientsViewCommand = new NavigateCommand<PatientsViewModel>(ExecuteShowPatientsViewCommand);
        
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
}