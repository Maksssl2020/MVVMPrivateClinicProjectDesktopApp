using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVMPrivateClinicProjectDesktopApp.Views.Components;

public partial class ButtonWithIcon : UserControl {
    public static readonly DependencyProperty IconNameProperty = 
        DependencyProperty.Register(nameof(IconName), typeof(string), typeof(ButtonWithIcon),
            new PropertyMetadata("Plus"));

    public static readonly DependencyProperty TextColorProperty = 
        DependencyProperty.Register(nameof(TextColor), typeof(SolidColorBrush), typeof(ButtonWithIcon),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));
    
    public static readonly DependencyProperty ButtonTextProperty = 
        DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(ButtonWithIcon),
            new PropertyMetadata("Add"));
    
    public string IconName {
        get => (string)GetValue(IconNameProperty);
        set => SetValue(IconNameProperty, value);
    }

    public SolidColorBrush TextColor {
        get => (SolidColorBrush) GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public string ButtonText {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }
    
    public ButtonWithIcon(){
        InitializeComponent();
    }
}