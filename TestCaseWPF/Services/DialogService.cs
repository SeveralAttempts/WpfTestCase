using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using TestCaseWPF.Services.Interfaces;

namespace TestCaseWPF.Services
{
    internal class DialogService : IDialogService
    {
        public bool OpenFileDialog(ref IFileSevice fileSevice)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Выберите изображение";
            openDialog.Multiselect = false;
            openDialog.Filter = "Image Files(*.jpeg; *.png; *.bmp)|*.jpeg; *.png; *.bmp";
            if (openDialog.ShowDialog() == true)
            {
                fileSevice.File = openDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog(ref IFileSevice fileSevice)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Сохраните изображение";
            saveDialog.Filter = "Image Files(*.jpeg; *.png; *.bmp)|*.jpeg; *.png; *.bmp";
            if (saveDialog.ShowDialog() == true)
            {
                fileSevice.File = saveDialog.FileName;

                return true;
            }
            return false;
        }
    }
}
