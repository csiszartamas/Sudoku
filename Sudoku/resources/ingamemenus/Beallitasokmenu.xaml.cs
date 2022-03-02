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
    /// Interaction logic for Beallitasokmenu.xaml
    /// </summary>
    public partial class Beallitasokmenu : Window
    {
        public string Felhasznalonev { get; set; }
        public Beallitasokmenu(string felhasznalonev)
        {
            InitializeComponent();
            Felhasznalonev = felhasznalonev;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TB_felhasznalonev.Text = $"{Felhasznalonev}";
        }
    }
}
