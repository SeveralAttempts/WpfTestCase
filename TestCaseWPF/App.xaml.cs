using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;
using TestCaseWPF.Services;
using TestCaseWPF.ViewModels;
using TestCaseWPF.Views.Windows;

namespace TestCaseWPF
{
    public partial class App : Application
    {
        HistogramWindow _histogramwindow;
        MainWindow _mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _histogramwindow = new();
            _mainWindow = new();

            HistogramWindowViewModel _histogram = new HistogramWindowViewModel();
            MainWindowViewModel _main = new MainWindowViewModel(_histogramwindow, new DialogService(), new ImageService(), new ImageProcessingService<float>());

            _histogramwindow.DataContext = _histogram;
            _mainWindow.DataContext = _main;

            _main.Ids += _histogram.Update;
            _histogram.PositionWhenEnter += _main.UpdateImageByPixels;
            _histogram.Restore += _main.RestoreImage;
            _mainWindow.Show();
        }
    }
}
