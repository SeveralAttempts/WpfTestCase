using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using OpenCvSharp;
using TestCaseWPF.Infrastructure;
using TestCaseWPF.Services;
using TestCaseWPF.Services.Interfaces;
using TestCaseWPF.ViewModels.Abstract;

namespace TestCaseWPF.ViewModels
{
    internal class HistogramWindowViewModel : ViewModel
    {
        private string _histogramWindowTitle = "Гисторграмма";
        public string HistogramWindowTitle { get => _histogramWindowTitle; set => Set(ref _histogramWindowTitle, value); }

        IImageProcessingService<float> _imageProcessingService { get; set; }
        

        public event EventHandler<PositionEventArgs> PositionWhenEnter;
        public event EventHandler Restore;


        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }


        private List<float> _histGrayScaleValues;


        private Canvas _histogramCanvas;
        public Canvas HistogramCanvas { get => _histogramCanvas; set => Set(ref _histogramCanvas, value); }

        public void Update(object sender, HistogramEventArgs<float> e)
        {
            HistMakeChange((int)HistogramCanvas.Width);
        }

        private void HistMakeChange(int windowWidth)
        {
            _imageProcessingService.MakeHistogram(windowWidth);
            _histGrayScaleValues = _imageProcessingService.HistGrayScaleValues;
            double coefMax = _imageProcessingService.MaxPixelDensity / HistogramCanvas.Height;
            for (int i = 0; i < _histGrayScaleValues.Count; i++)
            {
                var rect = HistogramCanvas.Children[i] as Rectangle;
                rect.Width = 1;
                rect.Height = _histGrayScaleValues.ElementAt(i) / coefMax;
                rect.Fill = Brushes.Black;
                rect.Margin = new Thickness(i, HistogramCanvas.Height, 0, 0);
                rect.RenderTransform = new ScaleTransform() { ScaleY = -1 };
            }
        }

        private void OnRectangleMouseEnter(ushort PixelColorRange)
        {
            PositionWhenEnter?.Invoke(this, new PositionEventArgs() { Position = PixelColorRange, CanvasWidth = (ushort)HistogramCanvas.Width });
        }

        private void OnRectangleMouseLeave()
        {
            Restore?.Invoke(this, new EventArgs());
        }

        public HistogramWindowViewModel(IImageProcessingService<float> imageProcessingService)
        {
            _histGrayScaleValues = new();
            HistogramCanvas = new Canvas();
            HistogramCanvas.Margin = new Thickness(31, 0, 31, 40);
            HistogramCanvas.MinWidth = 600;
            HistogramCanvas.MinHeight = 300;
            HistogramCanvas.Width = 600;
            HistogramCanvas.Height = 300;
            HistogramCanvas.Background = Brushes.Lavender;
            for (int i = 0; i < HistogramCanvas.Width; i++)
            {
                var rect = new Rectangle();
                rect.MouseEnter += (s, e) =>
                {
                    var targetItem = s as Rectangle;
                    targetItem.Fill = Brushes.Red;
                    ushort position = (ushort)(HistogramCanvas.Children.IndexOf(targetItem) + 1);
                    OnRectangleMouseEnter(position);

                };
                rect.MouseLeave += (s, e) =>
                {
                    var targetItem = s as Rectangle;
                    targetItem.Fill = Brushes.Black;
                    OnRectangleMouseLeave();
                };
                HistogramCanvas.Children.Add(rect);
            }
            _imageProcessingService = imageProcessingService;
            WindowWidth = HistogramCanvas.Width * 1.5;
            WindowHeight = HistogramCanvas.Height * 1.5;
        }
    }
}
