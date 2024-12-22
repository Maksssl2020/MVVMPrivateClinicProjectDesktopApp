using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class MedicinesView : UserControl {
    public MedicinesView(){
        InitializeComponent();
    }
    
    private void SeeMedicineDetailsButton_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as MedicinesViewModel;
        if (sender is not Button { DataContext: MedicineDto medicine }) return;
        if (viewModel is null) return;
        
        viewModel.SetMedicineIdToShowDetails(medicine.Id);
        viewModel.ShowMedicineDetailsModal.Execute(null);
    }
}