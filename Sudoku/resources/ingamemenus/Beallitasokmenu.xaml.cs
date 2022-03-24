using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// 
    public partial class Beallitasokmenu : Window
    {
        public string ConnectionString { get; set; }
        public string Felhasznalonev { get; set; }
        public string Jatekosnev { get; set; }
        public Beallitasokmenu(string felhasznalonev,string jatekosnev)
        {
            ConnectionString =
                @"Server   = (localdb)\MSSQLLocalDB;" +
                 "Database = szakdolgozat;";
            InitializeComponent();
            Felhasznalonev = felhasznalonev;
            Jatekosnev = jatekosnev;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TB_felhasznalonev.Text = $"{Felhasznalonev}";
            TB_jatekosnev.Text = $"{Jatekosnev}";
        }

        private void BT_adatvaltas_Click(object sender, RoutedEventArgs e)
        {
            string jelenlegijelszo = TxtBlck_jelszo.Text;
            string ujjelszo = TxtBlck_ujjelszo.Text;
            string ujjelszomegerosites = TxtBlck_jelszomegerosites.Text;


        }
    }
}
