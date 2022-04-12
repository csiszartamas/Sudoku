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
using System.IO;

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
            Close();
        }

        private void BT_angol_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(2).Show();
            Close();
        }

        private void BT_angol_MouseEnter(object sender, MouseEventArgs e)
        {

            /*var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("resources/images/eng.png", UriKind.Relative));
            BT_angol.Background = brush;*/


        }
        private void OnSetBackground(object sender, RoutedEventArgs e)

        {

            Button button = sender as Button;

            ImageBrush brush = new ImageBrush();

            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();

            bitmap.UriSource = new Uri(@"..\..\resources\images\eng.jpg", UriKind.Absolute);

            bitmap.EndInit();

            brush.ImageSource = bitmap;

            button.Background = brush;

        }
    }
}
