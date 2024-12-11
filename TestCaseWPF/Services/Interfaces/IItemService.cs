using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseWPF.Services.Interfaces
{
    internal interface IItemService
    {
        public ObservableCollection<int> Items { get; set; }
    }
}
