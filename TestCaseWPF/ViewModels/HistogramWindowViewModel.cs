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


        List<int> _histGrayScaleValues;


        private Canvas _histogramCanvas;
        public Canvas HistogramCanvas { get => _histogramCanvas; set => Set(ref _histogramCanvas, value); }

        public void Update(object sender, HistogramEventArgs e)
        {
            _histGrayScaleValues = e.HistogramValues;
            HistogramCanvas.Children.Clear();
            for (int i = 0; i < _histGrayScaleValues.Count; i++)
            {
                var rect = new Rectangle();
                rect.Width = 7;
                rect.Height = _histGrayScaleValues.ElementAt(i);
                rect.Fill = Brushes.Black;
                rect.Margin = new Thickness(i, 300, 0, 0);
                rect.RenderTransform = new ScaleTransform() { ScaleY = -1 };
                HistogramCanvas.Children.Add(rect);
            }
        }

        public HistogramWindowViewModel()
        {
            _histGrayScaleValues = new();
            HistogramCanvas = new Canvas();
            HistogramCanvas.Margin = new Thickness(31, 0, 31, 40);
            HistogramCanvas.Width = 600;
            HistogramCanvas.Height = 300;
            HistogramCanvas.Background = Brushes.Lavender;
        }
    }
}
