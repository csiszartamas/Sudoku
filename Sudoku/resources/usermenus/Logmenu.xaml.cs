using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Logmenu.xaml
    /// </summary>
    public partial class Logmenu : Window
    {
        public int Nyelv { get; set; }
        public string ConnectionString { get; set; }
        public Logmenu(int nyelv)
        {
            ConnectionString =
                @"Server   = (localdb)\MSSQLLocalDB;" +
                 "Database = szakdolgozat;";
            InitializeComponent();
            Nyelv = nyelv;
        }


        private void BT_register(object sender, RoutedEventArgs e)
        {
            new Regmenu(Nyelv).Show();
            Close();
        }

        private void BT_login_Click(object sender, RoutedEventArgs e)
        {
            if (TB_felhasznalonev.Text.Length == 0)
            {
                errormessage.Text = "Adj meg egy felhasználónevet!";
                TB_felhasznalonev.Focus();
            }
            else if (TB_felhasznalonev.Text.Length <= 3)
            {
                errormessage.Text = "Érvénytelen felhasználónév! (Minimum 4 karakter!)";
                TB_felhasznalonev.Focus();
            }
            else if (PB_jelszo.Password.Length == 0)
            {
                errormessage.Text = "Adjon meg jelszavat!";
                PB_jelszo.Focus();
            }
            else if (PB_jelszo.Password.Length <= 7)
            {
                errormessage.Text = "Legalább 8 karakter legyen a jelszava!";
                PB_jelszo.Focus();
            }
            else
            {
                string felhasznalonev = TB_felhasznalonev.Text;
                string jelszo = PB_jelszo.Password;
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select COUNT(*) from jatekos where felhasznalonev = '" + felhasznalonev + "' and jelszo = '" + jelszo + "'", con);
                DataTable datatable = new DataTable();
                sda.Fill(datatable);
                if (datatable.Rows[0][0].ToString() == "1")
                {
                    this.Hide();
                    new Sudoku_game(felhasznalonev).Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!");
                }
            }
        }

        private void PB_jelszo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                BT_login_Click(sender, e);
            }
        }

        private void TB_felhasznalonev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                BT_login_Click(sender, e);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (Nyelv == 1)
            {
                TxtBlck_felhasznalonev.Text = "Felhasználónév: ";
                TxtBlck_jelszo.Text = "Jelszó: ";
                BT_login.Content = "Bejelentkezés";
            }
            else if (Nyelv == 2)
            {
                TxtBlck_felhasznalonev.Text = "Username: ";
                TxtBlck_jelszo.Text = "Passsword: ";
                BT_login.Content = "Login";
            }
        }

        private void BT_login_MouseEnter(object sender, MouseEventArgs e)
        {
            BT_login.Background = new SolidColorBrush(Colors.Blue);
        }

        private void BT_login_MouseLeave(object sender, MouseEventArgs e)
        {
            BT_login.Background = new SolidColorBrush(Colors.DodgerBlue);
        }
    }
}
