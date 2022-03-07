using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for SzamBeiro.xaml
    /// </summary>
    public partial class SzamBeiro : Window
    {
        public int Id;
        public SzamBeiro(int id)
        {
            InitializeComponent();
            Id = id;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Label newBtn = new Label();
            newBtn.Content = "Button"+Id;
            newBtn.Width = 40;
            newBtn.Height = 40;
        }
    }
}
