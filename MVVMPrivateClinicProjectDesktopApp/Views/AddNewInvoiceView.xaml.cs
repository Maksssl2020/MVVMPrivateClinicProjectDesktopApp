using System.Windows;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewInvoiceView : WindowViewBase {
    public AddNewInvoiceView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        PatientSelector.SelectedItem = null;
        PricingSelector.SelectedItem = null;
    }
}