using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TestCaseWPF.Services;
using TestCaseWPF.Services.Interfaces;
using TestCaseWPF.Support;

namespace TestCaseWPF
{
    internal class ViewModel : INotifyPropertyChanged
    {
        IDialogService _dialogService;
        IFileSevice _fileSevice;

        public ObservableCollection<FilterItem> Filters { get; set; }

        BitmapImage _image;
        public BitmapImage DisplayedImage 
        { 
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        public FilterItem SelectedFilter { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        BaseCommand _openDialogCommand;
        public BaseCommand OpenDialogCommand
        {
            get
            {
                return _openDialogCommand ??
                    (_openDialogCommand = new BaseCommand(o =>
                    {
                        if (_dialogService.OpenFileDialog(ref _fileSevice) == true)
                        {
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(_fileSevice.File);
                            bitmap.EndInit(); 
                            DisplayedImage = bitmap;
                        }
                    }));
            }
        }

        BaseCommand _saveDialogCommand;
        public BaseCommand SaveDialogCommand
        {
            get
            {
                return _saveDialogCommand ??
                    (_saveDialogCommand = new BaseCommand(o =>
                    {
                        if (_dialogService.SaveFileDialog(ref _fileSevice) is true)
                        {

                        }
                    }));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ViewModel(IDialogService dialogService, IFileSevice fileSevice)
        {
            _dialogService = dialogService;
            _fileSevice = fileSevice;

            Filters = new ObservableCollection<FilterItem>
            {
                new FilterItem() { Filter =  "Черно-белый фильтр" },
                new FilterItem() { Filter =  "Медианный фильтр" }
            };
        }
    }
}
