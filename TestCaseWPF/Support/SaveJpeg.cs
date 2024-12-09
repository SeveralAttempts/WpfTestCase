using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TestCaseWPF.Support.Interfaces;

namespace TestCaseWPF.Support
{
    internal class SaveJpeg : ISaveImage
    {
        public void Save(string fileName, BitmapSource source)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(source));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
                encoder.Save(stream);
        }
    }
}
