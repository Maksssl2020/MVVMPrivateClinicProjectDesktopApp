using System.Drawing;
using System.Windows;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Application = System.Windows.Application;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindowView : Window {
    public MainWindowView(){
        InitializeComponent();
        ResizeMode = ResizeMode.NoResize;
    }

    private void MouseLeftButtonDown_Press(object sender, MouseButtonEventArgs e) => DragMove();
    private void buttonClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    private void buttonMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void GeneratePdf(object sender, RoutedEventArgs e){
        using var document = new PdfDocument();
        var page = document.Pages.Add();
        var graphics = page.Graphics;
        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            
        graphics.DrawString("Hello World!", font, PdfBrushes.Black, new PointF(0,0));
        document.Save("MyDocument.pdf");
    }
}