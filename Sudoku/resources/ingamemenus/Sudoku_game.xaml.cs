﻿using System;
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
        Random rnd = new Random();
        Random random = new Random();
        public int[,] grid = new int[9, 9];
        static string squarevalues;
        public string szambeiroid;
        public int value;
        public string content;

        List<Button> myButtons = new List<Button>();
        List<int> myButtonList = new List<int>();

        int currentX = 0;
        int currentY = 0;
        const int STEP = 1;
        const int WIDTH = 40;
        const int HEIGHT = 40;
        
        bool isButtonsOnScreen = false;
        
        public Sudoku_game(string felhasznalonev)
        {
            InitializeComponent();
            Felhasznalonev = felhasznalonev;
        }

        SudokuCell[,] cells = new SudokuCell[9, 9];
        //var wrongCells = new List<SudokuCell>();

        private void createCells()
        {
            if (!isButtonsOnScreen)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        squarevalues = grid[i, j].ToString();
                        cells[i, j] = new SudokuCell();
                        cells[i, j].Content = squarevalues;
                        cells[i, j].Name = "Button" + i.ToString();
                        cells[i, j].Width = WIDTH;
                        cells[i, j].Height = HEIGHT;
                        cells[i, j].KeyDown += cell_keyPressed;
                        cells[i, j].Background = ((i / 3) + (j / 3)) % 2 == 0 ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.White);
                        myButtons.Add(cells[i, j]);
                        Canvas.SetLeft(cells[i, j], STEP * currentX);
                        Canvas.SetTop(cells[i, j], STEP * currentY);
                        currentX += WIDTH;
                        MainCanvas.Children.Add(cells[i, j]);
                        szambeiroid = grid[i, j].ToString();
                    }
                    currentX = 0;
                    currentY += HEIGHT;
                }
            }
            else
            {

            }
        }

        private void cell_keyPressed(object sender, KeyEventArgs e)
        {
            var cell = sender as SudokuCell;
            // https://playwithcsharpdotnet.blogspot.com/2020/07/develop-sudoku-game-using-basic-csharp-codes.html
            // Do nothing if the cell is locked
            if (cell.IsLocked)
                return;

            int value;

            // Add the pressed key value in the cell only if it is a number
            if (int.TryParse(e.Key.ToString(), out value))
            {
                // Clear the cell value if pressed key is zero
                if (value == 0)
                    cell.Clear();
                else
                    cell.Content = value.ToString();

               // cell.ForeColor = SystemColors.ControlDarkDark;
            }
        }
        private void startNewGame()
        {
            loadValues();
            //Show values of 45 cells as hint
            showRandomValuesHints(45);
        }

        private void showRandomValuesHints(int hintsCount)
        {
            // Show value in random cells
            // The hints count is based on the level player choose
            for (int i = 0; i < hintsCount; i++)
            {
                var rX = random.Next(9);
                var rY = random.Next(9);

                // Style the hint cells differently and
                // lock the cell so that player can't edit the value
                cells[rX, rY].Content = cells[rX, rY].Value.ToString();
                //cells[rX, rY].ForeColor = Color.Black;
                cells[rX, rY].IsLocked = true;
            }
        }

        private void loadValues()
        {
            // Clear the values in each cells
            foreach (var cell in cells)
            {
                cell.Value = 0;
                cell.Clear();
            }

            // This method will be called recursively 
            // until it finds suitable values for each cells
            findValueForNextCell(0, -1);
        }

        private bool findValueForNextCell(int i, int j)
        {
            // Increment the i and j values to move to the next cell
            // and if the columsn ends move to the next row
            if (++j > 8)
            {
                j = 0;

                // Exit if the line ends
                if (++i > 8)
                    return true;
            }

            var value = 0;
            var numsLeft = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Find a random and valid number for the cell and go to the next cell 
            // and check if it can be allocated with another random and valid number
            do
            {
                // If there is not numbers left in the list to try next, 
                // return to the previous cell and allocate it with a different number
                if (numsLeft.Count < 1)
                {
                    cells[i, j].Value = 0;
                    return false;
                }

                // Take a random number from the numbers left in the list
                value = numsLeft[random.Next(0, numsLeft.Count)];
                cells[i, j].Value = value;

                // Remove the allocated value from the list
                numsLeft.Remove(value);
            }
            while (!isValidNumber(value, i, j) || !findValueForNextCell(i, j));

            // TDO: Remove this line after testing
            cells[i, j].Content = value.ToString();

            return true;
        }

        private bool isValidNumber(int value, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                // Check all the cells in vertical direction
                if (i != y && cells[x, i].Value == value)
                    return false;

                // Check all the cells in horizontal direction
                if (i != x && cells[i, y].Value == value)
                    return false;
            }

            // Check all the cells in the specific block
            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                {
                    if (i != x && j != y && cells[i, j].Value == value)
                        return false;
                }
            }

            return true;
        }
        private void BT_ujjatek_Click(object sender, RoutedEventArgs e)
        {
            createCells();
            startNewGame();
            //squarevalues = "";
            //Init(ref grid);
            //Update(ref grid, 10);
            //GenerateButtons(ref grid,squarevalues);
            //RemoveButtons();
        }
        /*
        private void RemoveButtons()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    
                }
            }
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
        }*/

        

        private void BT_remove_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in cells)
            {
                MainCanvas.Children.Remove(item);
            }
            isButtonsOnScreen = false;
            currentX = 0;
            currentY = 0;
            
        }

        /*

        private void GenerateButtons(ref int[,] grid, string _s)
        {
            
            if (!isButtonsOnScreen)
            {
                Createbuttons(ref grid, squarevalues);
            }
            else
            {
                foreach (var item in myButtons)
                {
                    MainCanvas.Children.Remove(item);
                }
                isButtonsOnScreen = false;
                currentX = 0;
                currentY = 0;
                Createbuttons(ref grid, squarevalues);
            }
        }

        private void Createbuttons(ref int[,] grid, string _s)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    squarevalues = grid[i,j].ToString();
                    //myButtonList[,] =;
                    int num = rnd.Next(100);
                    if (num < 30)
                    {
                        //squarevalues = "";
                    }
                    Button newBtn = new Button();
                    //newBtn.Content = i.ToString();
                    newBtn.Content = squarevalues;
                    newBtn.Name = "Button" + i.ToString();
                    newBtn.Width = WIDTH;
                    newBtn.Height = HEIGHT;
                    newBtn.Click += Interakcio;
                    myButtons.Add(newBtn);
                    Canvas.SetLeft(newBtn, STEP * currentX);
                    Canvas.SetTop(newBtn, STEP * currentY);
                    currentX += WIDTH;
                    MainCanvas.Children.Add(newBtn);
                    szambeiroid = grid[i,j].ToString();
                }
                currentX = 0;
                currentY += HEIGHT;
            }
            _s = squarevalues;
            //squarevalues = "";
            isButtonsOnScreen = true;
            
        }

        private void Interakcio(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(szambeiroid);
            var frm = new SzamBeiro(id);
            MessageBox.Show(""+id);
            //frm.ShowDialog();

            //MessageBox.Show(squarename);
        }

        private void Update(ref int[,] grid, int shuffleLevel)
        {
            for (int repeat = 0; repeat < shuffleLevel; repeat++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Random rand2 = new Random(Guid.NewGuid().GetHashCode());
                ChangeTwoCell(ref grid, rand.Next(1, 9), rand2.Next(1, 9));
            }
        }

        private void ChangeTwoCell(ref int[,] grid, int findValue1, int findValue2)
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

        */

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
