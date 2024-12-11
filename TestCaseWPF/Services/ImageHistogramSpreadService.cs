using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseWPF.Services.Interfaces;

namespace TestCaseWPF.Services
{
    internal class ImageHistogramSpreadService : IItemService
    {
        public ObservableCollection<int> Items { get; set; } = new();
    }
}
