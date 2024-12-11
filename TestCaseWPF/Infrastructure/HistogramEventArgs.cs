using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseWPF.Infrastructure
{
    class HistogramEventArgs : EventArgs
    {
        public List<int> HistogramValues { get; set; }

        public HistogramEventArgs(List<int> histogramValues)
        {
            HistogramValues = histogramValues;
        }
    }
}
