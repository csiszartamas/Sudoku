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
    /// Interaction logic for Sudoku_game.xaml
    /// </summary>
    public partial class Sudoku_game : Window
    {
        public Sudoku_game()
        {
            InitializeComponent();
        }

        private void BT_ujjatek_Click(object sender, RoutedEventArgs e)
        {
            //int szamlalo = 0;
            Countdown(0, TimeSpan.FromSeconds(1), cur => TB_ido.Text = cur.ToString() + " Másodperc");
        }
        
        void buttonnullazo()
        {
            for (int i = 0; i < 10; i++)
            {
                
            }
        }

        void Countdown(int szamlalo, TimeSpan intervallum, Action<int> ts)
        {

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = intervallum;
            timer.Tick += (_, a) => {
                if (szamlalo++ == 9999)
                {
                    timer.Stop();
                }
                else
                {
                    ts(szamlalo);
                } };
            ts(szamlalo);
            timer.Start();
        }
    }
}
