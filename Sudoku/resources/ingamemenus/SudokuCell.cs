using System.Windows.Controls;

namespace Sudoku
{
    internal class SudokuCell : Button
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