using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TestCaseWPF.Infrastructure;
using TestCaseWPF.ViewModels.Abstract;

namespace TestCaseWPF.ViewModels
{
    internal class HistogramWindowViewModel : ViewModel
    {
        private string _histogramWindowTitle = "Гисторграмма";
        public string HistogramWindowTitle { get => _histogramWindowTitle; set => Set(ref _histogramWindowTitle, value); }
        

        public event EventHandler<PositionEventArgs> PositionWhenEnter;
        public event EventHandler Restore;


        private List<float> _histGrayScaleValues;


        private Canvas _histogramCanvas;
        public Canvas HistogramCanvas { get => _histogramCanvas; set => Set(ref _histogramCanvas, value); }

        public void Update(object sender, HistogramEventArgs<float> e)
        {
            _histGrayScaleValues = e.HistogramValues;
            double coefMax = e.MaxDensityValue / HistogramCanvas.Height;
            for (int i = 0; i < _histGrayScaleValues.Count; i++)
            {
                var rect = HistogramCanvas.Children[i] as Rectangle;
                rect.Width = 1;
                rect.Height = _histGrayScaleValues.ElementAt(i) / coefMax;
                rect.Fill = Brushes.Black;
                rect.Margin = new Thickness(i, HistogramCanvas.Height, 0, 0);
                rect.RenderTransform = new ScaleTransform() { ScaleY = -1 };
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

        public HistogramWindowViewModel()
        {
            _histGrayScaleValues = new();
            HistogramCanvas = new Canvas();
            HistogramCanvas.Margin = new Thickness(31, 0, 31, 40);
            HistogramCanvas.Width = 600;
            HistogramCanvas.Height = 300;
            HistogramCanvas.Background = Brushes.Lavender;
            for (int i = 0; i < HistogramCanvas.Width; i++)
            {
                HistogramCanvas.Children.Add(new Rectangle());
            }
        }
    }
}
