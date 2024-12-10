using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseWPF.Services.Interfaces;
using TestCaseWPF.Support;

namespace TestCaseWPF.Services
{
    internal class ImageService : IFileSevice
    {
        public string File { get; set; }
    }
}
