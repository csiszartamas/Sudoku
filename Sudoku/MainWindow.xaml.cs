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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public int Nyelv { get; set; }
        public MainWindow(int nyelv)
        {
            InitializeComponent();
            Nyelv = nyelv;
        }

        private void BT_regmenu_Click(object sender, RoutedEventArgs e)
        {
            if (Nyelv != 1 && Nyelv != 2)
            {
                errormessage.Text = "HIBA! Válasszon nyelvet, ha még nem választott!";
            }
            else
            {
                new Regmenu(Nyelv).Show();
                Close();
            }
            
        }

        private void BT_logmenu_Click(object sender, RoutedEventArgs e)
        {
            if (Nyelv != 1 && Nyelv != 2)
            {
                errormessage.Text = "HIBA! Válasszon nyelvet, ha még nem választott!";
            }
            else
            {
                new Logmenu(Nyelv).Show();
                Close();
            }
        }

        private void BT_magyar_Click(object sender, RoutedEventArgs e)
        {
            //Nyelv = 1;
            //BT_logmenu.Content = "Bejelentkezés";
            //BT_regmenu.Content = "Regisztráció";
        }

        private void BT_angol_Click(object sender, RoutedEventArgs e)
        {
            //Nyelv = 2;
            //BT_logmenu.Content = "Login";
            //BT_regmenu.Content = "Registration";
        }

        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            if(Nyelv == 1)
            {
                BT_logmenu.Content = "Bejelentkezés";
                BT_regmenu.Content = "Regisztráció";
            }
            else if(Nyelv == 2)
            {
                BT_logmenu.Content = "Login";
                BT_regmenu.Content = "Registration";
            }
            else
            {
                MessageBox.Show("Hiba a MainWindow-ban", "Hibaüzenet");
            }
        }
    }
}
