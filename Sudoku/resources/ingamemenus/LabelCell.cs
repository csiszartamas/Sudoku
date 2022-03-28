using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku
{
    internal class LabelCell : Label
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Clear()
        {
            //this.Content = "a";
            this.Content = string.Empty;
            this.IsLocked = false;
            this.Value = 0;
        }
    }
}
