using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        

        public int[,] grid = new int[9, 9];
        static string s;

        public string content;

        List<Button> myButtons = new List<Button>();

        int currentX = 0;
        int currentY = 0;
        const int STEP = 1;
        const int WIDTH = 40;
        const int HEIGHT = 40;
        bool completed = false;
        string kiiras;
        bool isButtonsOnScreen = false;
        
        public Sudoku_game(string felhasznalonev)
        {
            InitializeComponent();
            Felhasznalonev = felhasznalonev;
            
            //GenerateButtons();


        }
        private void BT_ujjatek_Click(object sender, RoutedEventArgs e)
        {
            Init(ref grid);
            Update(ref grid, 10);
            GenerateButtons(ref grid);
            RemoveButtonText();
        }

        private void RemoveButtonText()
        {
            
        }

        static void Init(ref int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = (i * 3 + i / 3 + j) % 9 + 1;

                }
            }
        }

        

        private void BT_remove_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonsOnScreen)
            {
                foreach (var item in myButtons)
                {
                    MainCanvas.Children.Remove(item);
                }
                isButtonsOnScreen = false;
                currentX = 0;
                currentY = 0;
            }
        }
          
        

        private void GenerateButtons(ref int[,] grid)
        {
            if (!isButtonsOnScreen)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Button newBtn = new Button();
                        //newBtn.Content = i.ToString();
                        newBtn.Content += grid[i,j].ToString();
                        newBtn.Name = "Button" + i.ToString();
                        newBtn.Width = WIDTH;
                        newBtn.Height = HEIGHT;
                        myButtons.Add(newBtn);
                        
                        Canvas.SetLeft(newBtn, STEP * currentX);
                        Canvas.SetTop(newBtn, STEP * currentY);
                        currentX += WIDTH;
                        
                        MainCanvas.Children.Add(newBtn);
                    }
                    currentX = 0;
                    currentY += HEIGHT;
                }
                isButtonsOnScreen = true;
            }
        }

        static void Update(ref int[,] grid, int shuffleLevel)
        {
            for (int repeat = 0; repeat < shuffleLevel; repeat++)
            {
                Random rand = new Random();
                Random rand2 = new Random();
                ChangeTwoCell(ref grid, rand.Next(1, 9), rand2.Next(1, 9));
            }
        }

        static void ChangeTwoCell(ref int[,] grid, int findValue1, int findValue2)
        {
            
            int xParm1, yParm1, xParm2, yParm2;
            xParm1 = yParm1 = xParm2 = yParm2 = 0;
            for (int i = 0; i < 9; i += 3)
            {
                for (int k = 0; k < 9; k += 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (grid[i + j, k + z] == findValue1)
                            {
                                xParm1 = i + j;
                                yParm1 = k + z;

                            }
                            if (grid[i + j, k + z] == findValue2)
                            {
                                xParm2 = i + j;
                                yParm2 = k + z;

                            }
                        }
                    }
                    grid[xParm1, yParm1] = findValue2;
                    grid[xParm2, yParm2] = findValue1;
                }
            }
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
