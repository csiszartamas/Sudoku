using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Regmenu.xaml
    /// </summary>
   
    public partial class Regmenu : Window
    {
        public string ConnectionString { get; set; }
        public Regmenu()
        {

            ConnectionString =
                @"Server   = (localdb)\MSSQLLocalDB;" +
                 "Database = szakdolgozat;";
            InitializeComponent();
        }

        private void BT_registration_Click(object sender, RoutedEventArgs e)
        {
            if (TB_felhasznalonev.Text.Length == 0)
            {
                errormessage.Text = "Adjon meg felhasználónevet!";
                TB_email.Focus();
            }
            else if(TB_felhasznalonev.Text.Length < 3)
            {
                errormessage.Text = "Legalább 4 karakter legyen a felhasználóneve!";
                TB_email.Focus();
            }
            if (TB_jatekosnev.Text.Length == 0)
            {
                errormessage.Text = "Adjon meg Játékos nevet!";
                TB_email.Focus();
            }
            else if (TB_jatekosnev.Text.Length < 2)
            {
                errormessage.Text = "Legalább 3 karakter legyen a Játékos neve!";
                TB_email.Focus();
            }
            if (TB_email.Text.Length == 0)
            {
                errormessage.Text = "Adjon meg email címet!";
                TB_email.Focus();
            }
            else if (!Regex.IsMatch(TB_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Valódi email címet adjon meg!";
                TB_email.Select(0, TB_email.Text.Length);
                TB_email.Focus();
            }
            else
            {
                string felhasznalonev = TB_felhasznalonev.Text;
                string jatekosnev = TB_jatekosnev.Text;
                string email = TB_email.Text;
                string jelszo = PB_jelszo.Password;
                if (PB_jelszo.Password.Length == 0)
                {
                    errormessage.Text = "Adjon meg jelszavat!";
                    PB_jelszo.Focus();
                }
                else if (PB_jelszo.Password.Length == 0)
                {
                    errormessage.Text = "Erősítse meg a jelszavát!";
                    PB_jelszomegerosites.Focus();
                }
                else if (PB_jelszo.Password != PB_jelszo.Password)
                {
                    errormessage.Text = "Nem egyeznek a jelszavak! Erősítse meg újra!";
                    PB_jelszo.Focus();
                }
                else if (PB_jelszo.Password.Length < 7)
                {
                    errormessage.Text = "Legalább 8 karakter legyen a jelszava!";
                    PB_jelszo.Focus();
                }
                else
                {
                    
                    SqlConnection con = new SqlConnection(ConnectionString);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select count(felhasznalonev) from jatekos where felhasznalonev = '" + felhasznalonev + "'", con);
                    DataTable datatable = new DataTable();
                    sda.Fill(datatable);
                    if (int.Parse(datatable.Rows[0][0].ToString()) >= 1)
                    {
                        MessageBox.Show("Ilyen felhasználónév létezik már!");

                    }
                    else
                    {

                        try
                        {
                            using (var c = new SqlConnection(ConnectionString))
                            {
                                c.Open();
                                new SqlCommand($"Insert into jatekos (felhasznalonev,jelszo,email,jatekosnev,rangid,nyelvid) values('" + felhasznalonev + "','" + jelszo + "','" + email + "','" + jatekosnev + "',null,null)", c).ExecuteNonQuery();

                            }

                            MessageBox.Show("Sikeres regisztráció!");
                            new Logmenu().Show();
                            Close();
                        }
                        catch
                        {
                            MessageBox.Show("db error");
                        }
                    }
                    errormessage.Text = "";
                    
                }
            }
        }

        private void BT_login_Click(object sender, RoutedEventArgs e)
        {
            new Logmenu().Show();
            Close();
        }
    }
}
