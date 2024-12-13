using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Aruco;
using OpenCvSharp.WpfExtensions;
using TestCaseWPF.Services.Interfaces;

namespace TestCaseWPF.Services
{
    internal class ImageProcessingService<T> : IImageProcessingService<T> where T : struct
    {
        public List<T> HistGrayScaleValues { get; set; }
        public T MaxPixelDensity { get; set; }
        public Mat SourceMat { get; set; }
        public Mat FilteredMat { get; set; }

        public void MakeHistogram()
        {
            const int histogramSize = 600;
            Mat hist = new Mat();
            int[] dimensions = { histogramSize };
            Rangef[] ranges = { new Rangef(0, ushort.MaxValue) };
            Cv2.CalcHist(
                images: new[] { SourceMat },
                channels: new[] { 0 },
                mask: null,
                hist: hist,
                dims: 1,
                histSize: dimensions,
                ranges: ranges);
            HistGrayScaleValues.Clear();
            for (int i = 0; i < histogramSize; i++)
            {
                HistGrayScaleValues.Add(hist.Get<T>(i));
            }
            MaxPixelDensity = HistGrayScaleValues.Max();
        }

        public void ColorPickedPixelRange((ushort start, ushort end) pixelColorRange)
        {
            Cv2.CvtColor(SourceMat, FilteredMat, ColorConversionCodes.GRAY2BGR);
            FilteredMat.GetArray<Vec3w>(out var pixels);
            for (int i = 0; i < pixels.Length; i++)
            {
                var vectorPixelColor = pixels[i];
                if ((vectorPixelColor.Item0 >= pixelColorRange.start && vectorPixelColor.Item0 <= pixelColorRange.end)
                    && (vectorPixelColor.Item1 >= pixelColorRange.start && vectorPixelColor.Item1 <= pixelColorRange.end)
                    && (vectorPixelColor.Item2 >= pixelColorRange.start && vectorPixelColor.Item2 <= pixelColorRange.end))
                {
                    pixels[i] = new Vec3w(0, 0, 65535);
                }
            }
            FilteredMat.SetArray(pixels);
        }

        public void MedianFilter()
        {
            FilteredMat = new Mat(SourceMat.Size(), SourceMat.Type());
            Cv2.MedianBlur(SourceMat, FilteredMat, 7);
        }

        public ImageProcessingService()
        {
            HistGrayScaleValues = new List<T>();
            MaxPixelDensity = default;
        }
    }
}
