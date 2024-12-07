using System.Windows;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewPricingView : WindowViewBase {
    public AddNewPricingView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        ServiceNameInput.FormText = "";
        PriceInput.FormText = "";
        ServiceTypeSelector.SelectedItem = null;
    }
}