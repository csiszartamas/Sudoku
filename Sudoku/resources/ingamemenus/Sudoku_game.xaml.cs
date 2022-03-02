using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Sudoku_game.xaml
    /// </summary>
    public partial class Sudoku_game : Window
    {
        public string Felhasznalonev { get; set; }
        public Sudoku_game(string felhasznalonev)
        {
            InitializeComponent();
            Felhasznalonev = felhasznalonev;
        }

        private void BT_ujjatek_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LB_koszontes.Content = $"Üdvözöllek {Felhasznalonev}!";
        }

        private void BT_ranglista_Click(object sender, RoutedEventArgs e)
        {
            
            new Ranglistmenu().Show();
        }

        private void BT_beallitasok_Click(object sender, RoutedEventArgs e)
        {
            new Beallitasokmenu(Felhasznalonev).Show();
        }
    }
}
