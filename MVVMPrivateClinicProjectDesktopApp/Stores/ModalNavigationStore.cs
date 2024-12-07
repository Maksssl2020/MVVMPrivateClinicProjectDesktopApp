

using System.Windows;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ModalNavigationStore : NavigationStoreBase<ViewModelBase> {
    private Window CurrentModalView { get; set; } = null!;

    public void OpenModal(ViewModelBase viewModel, Window view){
        CurrentViewModel = viewModel;
        CurrentModalView = view;
        CurrentModalView.DataContext = viewModel;
        CurrentModalView.ShowDialog();
    }
    
    public void Close() {
        CurrentModalView.Close();
    }
}