using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVMPrivateClinicProjectDesktopApp.Views.Components;

public partial class SearchBar : UserControl {
    public static readonly DependencyProperty SearchBarTextProperty = 
        DependencyProperty.Register(nameof(SearchBarText), typeof(string), typeof(SearchBar),
            new PropertyMetadata(""));

    public static readonly DependencyProperty SearchBarTagProperty = 
        DependencyProperty.Register(nameof(SearchBarTag), typeof(object), typeof(SearchBar),
            new PropertyMetadata(null));
    
    public string SearchBarText {
        get => (string)GetValue(SearchBarTextProperty);
        set => SetValue(SearchBarTextProperty, value);
    }

    public object SearchBarTag {
        get => GetValue(SearchBarTagProperty);
        set => SetValue(SearchBarTagProperty, value);
    }
    
    public SearchBar(){
        InitializeComponent();
    }
}