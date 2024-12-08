using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class IssuePrescriptionView : UserControl {
    public IssuePrescriptionView(){
        InitializeComponent();
    }

    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
      
    }

    private void MedicinesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e){
        if (DataContext is not IssuePrescriptionViewModel viewModel) return;
        foreach (var addedItem in e.AddedItems) {
            if (addedItem is MedicineDto medicineDto && !viewModel.SelectedMedicines.Contains(medicineDto)) {
                viewModel.SelectedMedicines.Add(medicineDto);
            }
        }

        foreach (var removedItem in e.RemovedItems) {
            if (removedItem is MedicineDto medicineDto) {
                viewModel.SelectedMedicines.Remove(medicineDto);
            }
        }
    }
}