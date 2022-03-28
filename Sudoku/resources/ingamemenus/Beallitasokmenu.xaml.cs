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
    /// Interaction logic for Beallitasokmenu.xaml
    /// </summary>
    /// 
    public partial class Beallitasokmenu : Window
    {
        public string ConnectionString { get; set; }
        public string Felhasznalonev { get; set; }
        public string Jatekosnev { get; set; }
        public int Id { get; set; }
        public int Nyelv { get; set; }
        public Beallitasokmenu(int id,string felhasznalonev, string jatekosnev,int nyelv)
        {
            ConnectionString =
                @"Server   = (localdb)\MSSQLLocalDB;" +
                 "Database = szakdolgozat;";
            InitializeComponent();
            Felhasznalonev = felhasznalonev;
            Jatekosnev = jatekosnev;
            Id = id;
            Nyelv = nyelv;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TB_felhasznalonev.Text = $"{Felhasznalonev}";
            TB_jatekosnev.Text = $"{Jatekosnev}";
            if(Nyelv == 1)
            {
                TxtBlck_felhasznalonev.Text = "Felhasználó név:";
                TxtBlck_jatekosnev.Text = "Játékos név:";
                TxtBlck_jelszo.Text = "Jelenlegi jelszó:";
                TxtBlck_ujjelszo.Text = "Új jelszó:";
                TxtBlck_jelszomegerosites.Text = "Jelszó megerősítés";
            }
            else if(Nyelv == 2)
            {
                TxtBlck_felhasznalonev.Text = "User name:";
                TxtBlck_jatekosnev.Text = "Player name:";
                TxtBlck_jelszo.Text = "Current password:";
                TxtBlck_ujjelszo.Text = "New password:";
                TxtBlck_jelszomegerosites.Text = "Confirm password";
            }
            
        }

        private void BT_adatvaltas_Click(object sender, RoutedEventArgs e)
        {
            string jelenlegijelszo = PB_jelenlegijelszo.Password;
            string ujjelszo = PB_ujjelszo.Password;
            string ujjelszomegerosites = PB_ujjelszomegerosites.Password;
            if (ujjelszo != ujjelszomegerosites)
            {
                TxtBlck_jelszo.Focus();
                if (Nyelv == 1)
                {
                    errormessage.Text = "Nem egyezik az Új jelszavad és megerősítése!";
                }
                else if (Nyelv == 2)
                {
                    errormessage.Text = "Your new password and confirmation do not match!";
                }

            }
            else if (ujjelszo.Length < 7)
            {
                PB_jelenlegijelszo.Focus();
                if (Nyelv == 1)
                {
                    errormessage.Text = "Legalább 8 karakter legyen az új jelszava!";
                }
                else if (Nyelv == 2)
                {
                    errormessage.Text = "Your new password should be at least 8 characters long!";
                }
            }
            else
            {

                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select COUNT(*) from jatekos where felhasznalonev = '" + Felhasznalonev + "' and jelszo = '" + jelenlegijelszo + "'", con);
                DataTable datatable = new DataTable();
                sda.Fill(datatable);
                if (datatable.Rows[0][0].ToString() == "1")
                {
                    try
                    {
                        using (var c = new SqlConnection(ConnectionString))
                        {
                            c.Open();
                            new SqlCommand($"UPDATE jatekos SET jelszo = '{PB_ujjelszo.Password}' WHERE felhasznalonev = '{Felhasznalonev}';", c).ExecuteNonQuery();
                            if (Nyelv == 1)
                            {
                                MessageBox.Show("Sikeresen frissítetted!");
                            }
                            else if (Nyelv == 2)
                            {
                                MessageBox.Show("Successfully Update!");
                            }
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("DB ERROR!");
                    }
                }
                else
                {
                    if (Nyelv == 1)
                    {
                        MessageBox.Show("Hibás jelszó!");
                    }
                    else if (Nyelv == 2)
                    {
                        MessageBox.Show("Wrong Password!");
                    }
                }

            }
        }

        private void BT_magyar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var c = new SqlConnection(ConnectionString))
                {
                    c.Open();
                    new SqlCommand($"UPDATE jatekos SET nyelv = 1 WHERE felhasznalonev = '{Felhasznalonev}';", c).ExecuteNonQuery();
                    MessageBox.Show("Nyelv beállítva Magyarra!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("DB ERROR!");
            }
        }

        private void BT_angol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var c = new SqlConnection(ConnectionString))
                {
                    c.Open();
                    new SqlCommand($"UPDATE jatekos SET nyelv = 2 WHERE felhasznalonev = '{Felhasznalonev}';", c).ExecuteNonQuery();
                    MessageBox.Show("Language set to English!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("DB ERROR!");
            }
        }
    }
}
