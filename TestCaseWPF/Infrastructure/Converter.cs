using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace TestCaseWPF.Infrastructure
{
    internal static class Converter
    {
        public static Mat ToGray(Mat src)
        {
            int channels = src.Channels();
            int depth = src.Depth();

            if (channels == 1)
            {
                return src;
            }

            Mat gray = new Mat(src.Cols, src.Rows, MatType.MakeType(depth, 1));

            for (int y = 0; y < src.Rows; y++)
            {
                for (int x = 0; x < src.Cols; x++)
                {
                    
                }
            }

            return gray;
        }
    }
}
