using System.Windows;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class DeleteEntityView : Window {
    public DeleteEntityView(){
        InitializeComponent();
        ResizeMode = ResizeMode.NoResize;
        Loaded += DeleteEntityView_Loaded;
    }

    private void DeleteEntityView_Loaded(object sender, RoutedEventArgs e){
        if (DataContext is DeleteEntityViewModel viewModel) {
            viewModel.InitializeCloseModalEvent(Close);
        }
    }
    
    private void buttonClose_Click(object sender, RoutedEventArgs e) => Close();
    private void MouseLeftButtonDown_Hold(object sender, MouseButtonEventArgs e) => DragMove();
}