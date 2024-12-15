using System.Collections;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Brush = System.Drawing.Brush;
using Color = System.Drawing.Color;

namespace MVVMPrivateClinicProjectDesktopApp.Views.Components;

public partial class SortingComboBox : UserControl {
    public static readonly DependencyProperty ComboBoxBorderBrushProperty = 
        DependencyProperty.Register(nameof(ComboBoxBorderBrush), typeof(SolidColorBrush), typeof(SortingComboBox),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(SortingComboBox), new PropertyMetadata(null));
    
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(SortingComboBox), new PropertyMetadata(null));
    
    public SolidColorBrush ComboBoxBorderBrush {
        get => (SolidColorBrush)GetValue(ComboBoxBorderBrushProperty);
        set => SetValue(ComboBoxBorderBrushProperty, value);
    }
    
    public IEnumerable ItemsSource {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    public object SelectedItem {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    
    public SortingComboBox(){
        InitializeComponent();
    }
}