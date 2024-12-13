using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TestCaseWPF.Services;
using TestCaseWPF.Services.Interfaces;
using TestCaseWPF.ViewModels.Abstract;
using OpenCvSharp.WpfExtensions;
using OpenCvSharp;
using TestCaseWPF.Views.Windows;
using TestCaseWPF.Infrastructure;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TestCaseWPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private IDialogService _dialogService;
        private IFileSevice _fileSevice;
        private IImageProcessingService<float> _imageProcessingService;
        private System.Windows.Window _histogramWindow;


        public event EventHandler<HistogramEventArgs<float>> Ids;


        private string _mainWindowTitle = "Тестовое задание";
        public string MainWindowTitle { get => _mainWindowTitle; set => Set(ref _mainWindowTitle, value); }


        private double _appFontSize;
        public double AppFontSize
        {
            get => _appFontSize;
            set => Set(ref _appFontSize, value);
        }

        public double WindowWidth { get; init; }
        public double WindowHeight { get; init; }

        private double PrevWidth { get; set; }
        private double PrevHeight { get; set; }


        public ObservableCollection<FilterItem> Filters { get; set; }
        private readonly List<string> _possibleFormats;


        private ImageSource _imageSource;
        public ImageSource DisplayedImage
        {
            get => _imageSource;
            set => Set(ref _imageSource, value); 
        }


        //public UIElement.RenderSize RenderSize


        private FilterItem _selectedFilter;
        public FilterItem SelectedFilter { get => _selectedFilter; set => Set(ref _selectedFilter, value); }


        private BaseCommand _openDialogCommand;
        public BaseCommand OpenDialogCommand
        {
            get
            {
                return _openDialogCommand ??
                    (_openDialogCommand = new BaseCommand(o =>
                    {
                        if (_dialogService.OpenFileDialog(ref _fileSevice) == true)
                        {
                            _imageProcessingService.SourceMat = Cv2.ImRead(_fileSevice.File, ImreadModes.Unchanged);
                            DisplayedImage = _imageProcessingService.SourceMat.ToBitmapSource();
                        }
                        try
                        {
                            _imageProcessingService.FilteredMat = new Mat(_imageProcessingService.SourceMat.Size(), _imageProcessingService.SourceMat.Type());
                            //DisplayedImage = _imageProcessingService.SourceMat.ToBitmapSource();
                            DisplayedImage = _imageProcessingService.SourceMat.ToBitmapSource();
                            OnGrayScaleImageFiltered();
                        }
                        catch (ArgumentNullException)
                        {
                            MessageBox.Show("Выберите изображение");
                        }
                        catch (NullReferenceException)
                        {
                            MessageBox.Show("Выберите изображение");
                        }
                    }));
            }
        }

        private BaseCommand _saveDialogCommand;
        public BaseCommand SaveDialogCommand
        {
            get
            {
                return _saveDialogCommand ??
                    (_saveDialogCommand = new BaseCommand(o =>
                    {
                        if (_dialogService.SaveFileDialog(ref _fileSevice) == true)
                        {
                            string extension = Path.GetExtension(_fileSevice.File);
                            try
                            {
                                if (_possibleFormats.Contains(extension))
                                {
                                    if (_imageProcessingService.FilteredMat is not null)
                                        _imageProcessingService.FilteredMat.SaveImage(_fileSevice.File);
                                    else if (_imageProcessingService.SourceMat is not null)
                                        _imageProcessingService.SourceMat.SaveImage(_fileSevice.File);
                                    else
                                        MessageBox.Show("Не был выбран файл изображения");
                                }
                                else
                                {
                                    MessageBox.Show("Выберите другой формат");
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                MessageBox.Show("Не был выбран файл изображения");
                            }
                        }
                    }));
            }
        }

        private BaseCommand _rollBackImage;
        public BaseCommand RollBackImage
        {
            get
            {
                return _rollBackImage ??
                    (_rollBackImage = new BaseCommand(o =>
                    {
                        try
                        {
                            DisplayedImage = _imageProcessingService.SourceMat.ToBitmapSource();
                        }
                        catch (ArgumentNullException)
                        {
                            MessageBox.Show("Выберите изображение");
                        }
                    }));
            }
        }

        private BaseCommand _invokeHistogramWindow;
        public BaseCommand InvokeHistogramWindow
        {
            get
            {
                return _invokeHistogramWindow ??
                    (_invokeHistogramWindow = new BaseCommand(o =>
                    {
                        _histogramWindow.Show();
                    }));
            }
        }

        private BaseCommand _applyFilter;
        public BaseCommand ApplyFilter
        {
            get
            {
                return _applyFilter ??
                    (_applyFilter = new BaseCommand(o =>
                    {
                        if (_imageProcessingService.SourceMat is null)
                        {
                            MessageBox.Show("Выберите изображение");
                            return;
                        }
                        if (SelectedFilter.Identifier is ImageFilter.Median)
                        {
                            try
                            {
                                _imageProcessingService.MedianFilter();
                                DisplayedImage = _imageProcessingService.FilteredMat.ToBitmapSource();
                            }
                            catch (ArgumentNullException ex)
                            {
                                MessageBox.Show("Выберите изображение");
                            }
                            //catch (Exception ex)
                            //{
                            //    MessageBox.Show(ex.Message);
                            //}
                        }
                    }));
            }
        }

        private void OnGrayScaleImageFiltered()
        {
            Ids?.Invoke(this, new HistogramEventArgs<float>(_imageProcessingService.HistGrayScaleValues, _imageProcessingService.MaxPixelDensity));
        }

        public void UpdateImageByPixels(object sender, PositionEventArgs e)
        {
            (ushort start, ushort end) pixelColorRange;
            pixelColorRange.end = (ushort)Math.Ceiling((double)(ushort.MaxValue / (double)e.CanvasWidth * (double)e.Position));
            if (e.Position == 1)
            {
                pixelColorRange.start = 0;
                return;

            }
            pixelColorRange.start = (ushort)Math.Round((double)ushort.MaxValue / (double)e.CanvasWidth * (double)(e.Position - 1));
            _imageProcessingService.ColorPickedPixelRange(pixelColorRange);
            DisplayedImage = _imageProcessingService.FilteredMat.ToBitmapSource();
        }

        public void RestoreImage(object sender, EventArgs e)
        {
            DisplayedImage = _imageProcessingService.SourceMat.ToBitmapSource();
        }

        public MainWindowViewModel(System.Windows.Window histogramWindow, IDialogService dialogService, IFileSevice fileSevice, IImageProcessingService<float> imageProcessingService)
        {
            _dialogService = dialogService;
            _fileSevice = fileSevice;
            _imageProcessingService = imageProcessingService;
            _histogramWindow = histogramWindow;

            _possibleFormats = new List<string>
            {
                ".png", ".jpeg", ".bmp", ".tiff", "*.tif"
            };

            Filters = new ObservableCollection<FilterItem>
            {
                new FilterItem() { Filter =  "Медианный фильтр", Identifier = ImageFilter.Median }
            };

            SelectedFilter = Filters.ElementAt(0);

            WindowWidth = 800;
            WindowHeight = 550;
        }
    }
}
