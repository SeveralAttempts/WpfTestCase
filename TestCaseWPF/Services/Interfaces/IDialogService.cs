using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseWPF.Services.Interfaces
{
    internal interface IDialogService
    {
        bool OpenFileDialog(ref IFileSevice fileSevice);
        bool SaveFileDialog(ref IFileSevice fileSevice);
    }
}
