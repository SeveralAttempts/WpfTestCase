using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseWPF.Infrastructure
{
    internal class PositionEventArgs : EventArgs
    {
        public ushort Position { get; set; }
        public ushort CanvasWidth { get; set; }
    }
}
