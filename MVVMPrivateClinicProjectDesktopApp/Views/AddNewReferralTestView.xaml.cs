using System.Windows;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewReferralTestView : WindowViewBase {
    public AddNewReferralTestView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        ReferralTestNameInput.FormText = string.Empty;
        ReferralTestDescriptionInput.FormText = string.Empty;
    }
}