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
    /// Interaction logic for Nyelvvalaszto.xaml
    /// </summary>
    public partial class Nyelvvalaszto : Window
    {
        public Nyelvvalaszto()
        {
            InitializeComponent();
        }

        private void BT_magyar_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(1).Show();
        }

        private void BT_angol_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(2).Show();
        }
    }
}
