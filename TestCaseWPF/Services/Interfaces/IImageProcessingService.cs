using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace TestCaseWPF.Services.Interfaces
{
    internal interface IImageProcessingService<T> where T : struct
    {
        void MedianFilter();
        void MakeHistogram();
        List<T> HistGrayScaleValues { get; set; }
        T MaxPixelDensity { get; set; }
        Mat SourceMat { get; set; }
        Mat FilteredMat { get; set; }
    }
}
