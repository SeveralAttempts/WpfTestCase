using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
using TestCaseWPF.Services;
using TestCaseWPF.Services.Interfaces;
using TestCaseWPF.ViewModels;
using TestCaseWPF.Views.Windows;

namespace TestCaseWPF
{
    public partial class App : Application
    {
        HistogramWindow _histogramWindow;
        MainWindow _mainWindow;
        IImageProcessingService<float> _imageProcessingService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _histogramWindow = new();
            _mainWindow = new();

            _imageProcessingService = new ImageProcessingService<float>();

            HistogramWindowViewModel _histogram = new HistogramWindowViewModel(_imageProcessingService);
            MainWindowViewModel _main = new MainWindowViewModel(_histogramWindow, new DialogService(), new ImageService(), _imageProcessingService);

            _histogramWindow.DataContext = _histogram;
            _mainWindow.DataContext = _main;

            _main.Ids += _histogram.Update;
            _histogram.PositionWhenEnter += _main.UpdateImageByPixels;
            _histogram.Restore += _main.RestoreImage;

            _mainWindow.Show();
        }
    }
}
