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
        public string ConnectionString { get; set; }
        public Logmenu()
        {
            ConnectionString =
                @"Server   = (localdb)\MSSQLLocalDB;" +
                 "Database = szakdolgozat;";
            InitializeComponent();

        }


        private void BT_register(object sender, RoutedEventArgs e)
        {
            new Regmenu().Show();
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
            else if (PB_password.Password.Length == 0)
            {
                errormessage.Text = "Adjon meg jelszavat!";
                PB_password.Focus();
            }
            else if (PB_password.Password.Length <= 7)
            {
                errormessage.Text = "Legalább 8 karakter legyen a jelszava!";
                PB_password.Focus();
            }
            else
            {
                string felhasznalonev = TB_felhasznalonev.Text;
                string password = PB_password.Password;
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select COUNT(*) from jatekos where felhasznalonev = '" + felhasznalonev + "' and jelszo = '" + password + "'", con);
                DataTable datatable = new DataTable();
                sda.Fill(datatable);
                if (datatable.Rows[0][0].ToString() == "1")
                {
                    this.Hide();
                    new Sudoku_game().Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!");
                }
            }
        }
    }
}
