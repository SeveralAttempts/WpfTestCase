using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
        private List<Mat> SourceMats;
        private List<Mat> FilteredMats;
        private int _index;
        public Mat Source { get => SourceMats[_index]; }
        public Mat Filtered { get => FilteredMats[_index]; }

        public void MakeHistogram(int histogramSize)
        {
            Mat hist = new Mat();
            int[] dimensions = { histogramSize };
            Rangef[] ranges = { new Rangef(0, ushort.MaxValue) };
            Cv2.CalcHist(
                images: new[] { Source },
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
            FilteredMats[_index] = new Mat(Source.Size(), Source.Type());
            Cv2.CvtColor(Source, Filtered, ColorConversionCodes.GRAY2BGR);
            Filtered.GetArray<Vec3w>(out var pixels);
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
            Filtered.SetArray(pixels);
        }

        public void MedianFilter()
        {
            FilteredMats[_index] = new Mat(Source.Size(), Source.Type());
            Cv2.MedianBlur(Source, Filtered, 7);
        }

        public void AddImage(Mat newImage)
        {
            SourceMats.Add(newImage);
            FilteredMats.Add(newImage);
            if (SourceMats.Count == 1)
                _index = 0;
        }

        public void MoveNext()
        {
            _index++;
            if (_index < 0)
                _index = SourceMats.Count - 1;
            else if (_index >= SourceMats.Count)
                _index = 0;
        }

        public void MovePrev()
        {
            _index--;
            if (_index < 0)
                _index = SourceMats.Count - 1;
            else if (_index >= SourceMats.Count)
                _index = 0;
        }

        public ImageProcessingService()
        {
            HistGrayScaleValues = new List<T>();
            SourceMats = new List<Mat>();
            FilteredMats = new List<Mat>();
            MaxPixelDensity = default;
            _index = -1;
        }
    }
}
