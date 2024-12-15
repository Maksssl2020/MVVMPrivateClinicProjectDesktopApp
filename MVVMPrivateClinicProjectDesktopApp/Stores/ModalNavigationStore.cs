

using System.Windows;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ModalNavigationStore : NavigationStoreBase<ViewModelBase> {
    private Window? CurrentModalView { get; set; }

    public void OpenModal(ViewModelBase viewModel, Window? view){
        CurrentViewModel?.Dispose();
        CurrentViewModel = viewModel;
        
        CurrentModalView?.Close();
        CurrentModalView = view;
        
        if (CurrentModalView == null) return;
        
        CurrentModalView.DataContext = viewModel;
        CurrentModalView.ShowDialog();
    }
}