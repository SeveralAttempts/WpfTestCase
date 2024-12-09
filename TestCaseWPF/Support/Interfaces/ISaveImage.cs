using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestCaseWPF.Support.Interfaces
{
    internal interface ISaveImage
    {
        void Save(string fileName, BitmapSource source);
    }
}
