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
      PrescriptionDescriptionInput.FormText = "";
      DoctorSelector.SelectedItem = null;
      MedicinesSelector.SelectedItem = null;

      if (DataContext is not IssuePrescriptionViewModel viewModel) return;
      viewModel.SelectedMedicines = [];
    }

    private void MedicinesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e){
        if (DataContext is not IssuePrescriptionViewModel viewModel) return;
        var items = viewModel.SelectedMedicines;
        
        foreach (var addedItem in e.AddedItems) {
            if (addedItem is MedicineDto medicine) {
                items.Add(medicine);
            }
        }

        foreach (var removedItem in e.RemovedItems) {
            if (removedItem is MedicineDto medicineDto) {
                items.Remove(medicineDto);
            }
        }
        
        viewModel.SelectedMedicines = items;
    }
}