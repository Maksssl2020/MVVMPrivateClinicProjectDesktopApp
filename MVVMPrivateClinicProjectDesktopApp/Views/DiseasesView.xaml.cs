using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class DiseasesView : UserControl {
    public DiseasesView(){
        InitializeComponent();
    }
    
    private void SeeDiseaseDetailsButton_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as DiseasesViewModel;
        if (sender is not Button { DataContext: DiseaseDto disease }) return;
        if (viewModel is null) return;
        
        viewModel.SetEntityIdToShowDetails(disease.Id);
        viewModel.ShowDiseaseDetailsModalCommand.Execute(null);
    }
    
    private void ShowDeleteDiseaseModal_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as DiseasesViewModel;
        if (sender is not Button { DataContext: DiseaseDto disease }) return;

        viewModel?.SetEntityIdToDelete(disease.Id);
    }
}