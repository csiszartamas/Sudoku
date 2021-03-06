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
    /// Interaction logic for Ranglistmenu.xaml
    /// </summary>
    public partial class Ranglistmenu : Window
    {
        public string ConnectionString { get; set; }
        public int Nyelv { get; set; }
        public Ranglistmenu(int nyelv)
        {
            ConnectionString =
                @"Server   = (localdb)\MSSQLLocalDB;" +
                 "Database = szakdolgozat;";
            Nyelv = nyelv;
            InitializeComponent();

            FillDGV_Easy();
            FillDGV_Medium();
            FillDGV_Hard();
            
        }
        public struct MyData
        {
            public int id { set; get; }
            public string nev { set; get; }
            public int ido { set; get; }
        }


        private void FillDGV_Easy()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            myDataGrid_Easy.Columns.Add(col1);
            myDataGrid_Easy.Columns.Add(col2);
            myDataGrid_Easy.Columns.Add(col3);
            col1.Binding = new Binding("id");
            col2.Binding = new Binding("nev");
            col3.Binding = new Binding("ido");
            if (Nyelv == 1)
            {
                col1.Header = "Helyezés";
                col2.Header = "Játékos Név";
                col3.Header = "Idő";
                TB_Easy.Content = "Könnyü Szint";
                TB_Medium.Content = "Közepes szint";
                TB_Hard.Content = "Nehéz szint";
            }
            else if(Nyelv == 2)
            {
                col1.Header = "Ranking";
                col2.Header = "Player Name";
                col3.Header = "Time";
                TB_Easy.Content = "Easy level";
                TB_Medium.Content = "Medium level";
                TB_Hard.Content = "Hard level";
            }
            using (var c = new SqlConnection(ConnectionString))
            {  
                c.Open();
                var r = new SqlCommand("SELECT jatekos.jatekosnev,ranglista.ido FROM ranglista INNER JOIN jatekos ON ranglista.jatekosid = jatekos.id WHERE ranglista.nehezseg = 0 ORDER BY ranglista.ido ASC;", c).ExecuteReader();
                int helyezes = 1;
                while (r.Read())
                {
                    myDataGrid_Easy.Items.Add(new MyData { id = helyezes, nev = r[0].ToString(), ido = int.Parse(r[1].ToString())});
                    helyezes++;
                }
            }
        }
        private void FillDGV_Medium()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            myDataGrid_Medium.Columns.Add(col1);
            myDataGrid_Medium.Columns.Add(col2);
            myDataGrid_Medium.Columns.Add(col3);
            col1.Binding = new Binding("id");
            col2.Binding = new Binding("nev");
            col3.Binding = new Binding("ido");
            if (Nyelv == 1)
            {
                col1.Header = "Helyezés";
                col2.Header = "Játékos Név";
                col3.Header = "Idő";
            }
            else if (Nyelv == 2)
            {
                col1.Header = "Ranking";
                col2.Header = "Player Name";
                col3.Header = "Time";
            }

            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var r = new SqlCommand("SELECT jatekos.jatekosnev,ranglista.ido FROM ranglista INNER JOIN jatekos ON ranglista.jatekosid = jatekos.id WHERE ranglista.nehezseg = 1 ORDER BY ranglista.ido ASC;", c).ExecuteReader();
                int helyezes = 1;
                while (r.Read())
                {
                    myDataGrid_Medium.Items.Add(new MyData { id = helyezes, nev = r[0].ToString(), ido = int.Parse(r[1].ToString()) });
                    helyezes++;
                }
            }
        }
        private void FillDGV_Hard()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            myDataGrid_Hard.Columns.Add(col1);
            myDataGrid_Hard.Columns.Add(col2);
            myDataGrid_Hard.Columns.Add(col3);
            col1.Binding = new Binding("id");
            col2.Binding = new Binding("nev");
            col3.Binding = new Binding("ido");
            if (Nyelv == 1)
            {
                col1.Header = "Helyezés";
                col2.Header = "Játékos Név";
                col3.Header = "Idő";
            }
            else if (Nyelv == 2)
            {
                col1.Header = "Ranking";
                col2.Header = "Player Name";
                col3.Header = "Time";
            }

            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var r = new SqlCommand("SELECT jatekos.jatekosnev,ranglista.ido FROM ranglista INNER JOIN jatekos ON ranglista.jatekosid = jatekos.id WHERE ranglista.nehezseg = 2 ORDER BY ranglista.ido ASC;", c).ExecuteReader();
                int helyezes = 1;
                while (r.Read())
                {
                    myDataGrid_Hard.Items.Add(new MyData { id = helyezes, nev = r[0].ToString(), ido = int.Parse(r[1].ToString()) });
                    helyezes++;
                }
            }
        }

        private void myDataGrid_Easy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void myDataGrid_Medium_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void myDataGrid_Hard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
