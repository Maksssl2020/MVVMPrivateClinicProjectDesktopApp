using System.Windows;
using System.Windows.Controls;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class IssueReferralView : UserControl {
    public IssueReferralView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        ReferralNameInput.FormText = "";
        ReferralDescriptionInput.FormText = "";
        DoctorSelector.SelectedItem = null;
        DiseaseSelector.SelectedItem = null;
        ReferralTestSelector.SelectedItem = null;
    }
}