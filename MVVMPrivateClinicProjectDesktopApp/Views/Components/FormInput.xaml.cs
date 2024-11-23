using System.Windows;
using System.Windows.Controls;

namespace MVVMPrivateClinicProjectDesktopApp.Views.Components;

public partial class FormInput : UserControl {
    
    public static readonly DependencyProperty LabelTitleProperty = 
        DependencyProperty.Register("LabelTitle", typeof(string), typeof(FormInput),
            new PropertyMetadata(string.Empty));

    public string LabelTitle {
        get => (string)GetValue(LabelTitleProperty);
        set => SetValue(LabelTitleProperty, value);
    }
    
    public FormInput(){
        InitializeComponent();
    }
}