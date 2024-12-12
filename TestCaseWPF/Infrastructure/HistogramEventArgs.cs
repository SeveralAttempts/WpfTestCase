using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseWPF.Infrastructure
{
    class HistogramEventArgs<T> : EventArgs
    {
        public List<T> HistogramValues { get; set; }
        public T MaxDensityValue { get; set; }

        public HistogramEventArgs(List<T> histogramValues, T maxDensityValue)
        {
            HistogramValues = histogramValues;
            MaxDensityValue = maxDensityValue;
        }
    }
}
