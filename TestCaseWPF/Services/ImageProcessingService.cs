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
            Cv2.CvtColor(SourceMat, FilteredMat, ColorConversionCodes.GRAY2BGR);
            for (int i = 0; i < FilteredMat.Height / 2; i++)
            {
                for (int j = 0; j < FilteredMat.Width / 2; j++)
                {
                    FilteredMat.Set<Vec3f>(i, j, new Vec3f(0f, 0.9999f, 0f));
                }
            }
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
