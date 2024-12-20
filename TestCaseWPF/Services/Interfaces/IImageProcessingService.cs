﻿using System;
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
        void MakeHistogram(int histogramSize);
        void ColorPickedPixelRange((ushort start, ushort end) pixelColorRange);
        List<T> HistGrayScaleValues { get; set; }
        T MaxPixelDensity { get; set; }
        Mat Source { get; }
        Mat Filtered { get; }
        void MovePrev();
        void MoveNext();
        void AddImage(Mat newImage);
    }
}
