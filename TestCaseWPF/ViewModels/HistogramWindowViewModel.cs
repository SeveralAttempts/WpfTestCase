using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestCaseWPF.ViewModels.Abstract;

namespace TestCaseWPF.ViewModels
{
    internal class HistogramWindowViewModel : ViewModel
    {
        private string _histogramWindowTitle = "Гисторграмма";
        public string HistogramWindowTitle { get => _histogramWindowTitle; set => Set(ref _histogramWindowTitle, value); }


        private Canvas _histogramCanvas;
        public Canvas HistogramCanvas { get => _histogramCanvas; set => Set(ref _histogramCanvas, value); }

        public HistogramWindowViewModel()
        {
            HistogramCanvas = new Canvas();
            HistogramCanvas.Margin = new Thickness(31, 40, 31, 40);
        }
    }
}
