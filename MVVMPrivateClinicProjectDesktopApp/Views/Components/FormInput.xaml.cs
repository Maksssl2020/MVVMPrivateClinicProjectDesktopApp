using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVMPrivateClinicProjectDesktopApp.Views.Components;

public partial class FormInput : UserControl {
    
    public static readonly DependencyProperty LabelTitleProperty = 
        DependencyProperty.Register(nameof(LabelTitle), typeof(string), typeof(FormInput),
            new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty FormTextProperty = 
        DependencyProperty.Register(nameof(FormText), typeof(string), typeof(FormInput),
            new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty ErrorMessageProperty = 
        DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(FormInput),
            new PropertyMetadata(string.Empty));
    
    public string LabelTitle {
        get => (string)GetValue(LabelTitleProperty);
        set => SetValue(LabelTitleProperty, value);
    }

    public string FormText {
        get => (string)GetValue(FormTextProperty);
        set => SetValue(FormTextProperty, value);
    }

    public string ErrorMessage {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    } 
    
    public FormInput(){
        InitializeComponent();
    }


}