using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    
    public static readonly DependencyProperty CommandProperty = 
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ButtonWithIcon),
            new PropertyMetadata(null));
    
    public static readonly DependencyProperty ButtonWidthProperty = 
        DependencyProperty.Register(nameof(ButtonWidth), typeof(double), typeof(ButtonWithIcon),
            new PropertyMetadata(150.0d));
    
    public static readonly DependencyProperty ButtonHeightProperty = 
        DependencyProperty.Register(nameof(ButtonHeight), typeof(double), typeof(ButtonWithIcon),
            new PropertyMetadata(50.0d));
    
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

    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public double ButtonWidth {
        get => (double)GetValue(ButtonWidthProperty);
        set => SetValue(ButtonWidthProperty, value);
    }

    public double ButtonHeight {
        get => (double)GetValue(ButtonHeightProperty); 
        set => SetValue(ButtonHeightProperty, value);
    }
    
    public ButtonWithIcon(){
        InitializeComponent();
        InternalButton.Click += (s, e) => Click?.Invoke(this, e);
    }

    public event RoutedEventHandler? Click;
}