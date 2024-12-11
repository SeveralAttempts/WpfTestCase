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
        private System.Windows.Window _histogramWindow;


        private List<int> _histGrayScaleValues;


        public event EventHandler<HistogramEventArgs> Ids;


        private string _mainWindowTitle = "Тестовое задание";
        public string MainWindowTitle { get => _mainWindowTitle; set => Set(ref _mainWindowTitle, value); }


        public ObservableCollection<FilterItem> Filters { get; set; }
        private readonly List<string> _possibleFormats;


        public Mat SourceMat { get; set; }
        public Mat FilteredMat { get; set; }


        private ImageSource _imageSource;
        public ImageSource DisplayedImage
        {
            get => _imageSource;
            set => Set(ref _imageSource, value); 
        }


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
                            SourceMat = Cv2.ImRead(_fileSevice.File);
                            DisplayedImage = SourceMat.ToBitmapSource();
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
                                    if (FilteredMat is not null)
                                        FilteredMat.SaveImage(_fileSevice.File);
                                    else if (SourceMat is not null)
                                        SourceMat.SaveImage(_fileSevice.File);
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
                            DisplayedImage = SourceMat.ToBitmapSource();
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
                        if (SourceMat is null)
                        {
                            MessageBox.Show("Выберите изображение");
                            return;
                        }
                        if (SelectedFilter.Identifier is ImageFilter.BW)
                        {
                            try
                            {
                                FilteredMat = new Mat(SourceMat.Size(), SourceMat.Type());
                                Cv2.CvtColor(SourceMat, FilteredMat, ColorConversionCodes.BGR2GRAY);
                                DisplayedImage = FilteredMat.ToBitmapSource();
                                Mat hist = new Mat();
                                int width = SourceMat.Cols, height = SourceMat.Rows;      // set Histogram same size as source image
                                const int histogramSize = 256;                      // you can change by urself
                                int[] dimensions = { histogramSize };               // Histogram size for each dimension
                                Rangef[] ranges = { new Rangef(0, 300) };
                                Cv2.CalcHist(
                                    images: new[] { FilteredMat },
                                    channels: new[] { 0 },
                                    mask: null,
                                    hist: hist,
                                    dims: 1,
                                    histSize: dimensions,
                                    ranges: ranges);
                                hist.SaveImage("C:\\Users\\coder7\\Downloads\\1_iYqZztXZhPBHQGZU1I0AzA.png");
                                //hist.ConvertTo(hist, MatType.CV_8UC1);
                                _histGrayScaleValues.Clear();
                                for (int i = 0; i < 255; i++)
                                {
                                    _histGrayScaleValues.Add(hist.Get<int>(i));
                                }
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
                        }
                        else
                        {
                            try
                            {
                                FilteredMat = new Mat(SourceMat.Size(), SourceMat.Type());
                                Cv2.MedianBlur(SourceMat, FilteredMat, 7);
                                DisplayedImage = FilteredMat.ToBitmapSource();
                            }
                            catch (ArgumentNullException ex)
                            {
                                MessageBox.Show("Выберите изображение");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }));
            }
        }

        private void OnGrayScaleImageFiltered()
        {
            Ids?.Invoke(this, new HistogramEventArgs(_histGrayScaleValues));
        }

        public MainWindowViewModel(System.Windows.Window histogramWindow, IDialogService dialogService, IFileSevice fileSevice)
        {
            _dialogService = dialogService;
            _fileSevice = fileSevice;
            _histogramWindow = histogramWindow;
            _histGrayScaleValues = new();

            _possibleFormats = new List<string>
            {
                ".png", ".jpeg", ".bmp" 
            };

            Filters = new ObservableCollection<FilterItem>
            {
                new FilterItem() { Filter =  "Черно-белый фильтр", Identifier = ImageFilter.BW },
                new FilterItem() { Filter =  "Медианный фильтр", Identifier = ImageFilter.Median }
            };

            SelectedFilter = Filters.ElementAt(0);
        }
    }
}
