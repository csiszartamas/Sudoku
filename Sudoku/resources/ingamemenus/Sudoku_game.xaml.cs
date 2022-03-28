using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Timers;
using System.Windows.Threading;
using System.Data.SqlClient;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for Sudoku_game.xaml
    /// </summary>
    public partial class Sudoku_game : Window
    {
        public string Felhasznalonev { get; set; }
        public string ConnectionString { get; set; }
        public int Id { get; set; }
        public int nehezseg { get; set; }

        Random rnd = new Random();
        public int showrandomvalueshints = 200;
        SudokuCell[,] cells = new SudokuCell[9, 9];
        SudokuCell[,] Numpads = new SudokuCell[3, 3];
        LabelCell[,] labelCells = new LabelCell[9, 9];
        LabelCell SelectedCellHint;
        SudokuCell SelectedCell;
        SudokuCell NumpadCell;
        Button newBtn = new Button();
        DispatcherTimer timer = new DispatcherTimer();
        int Nyelv = 0;
        int minute = 0;
        int second = 0;
        int hintsCount;
        int currentX_X = 0;
        int currentY_Y = 0;
        int currentX = 0;
        int currentY = 0;
        const int STEP = 1;
        const int WIDTH = 40;
        const int HEIGHT = 40;
        string Jatekosnev;
        bool gamefinished = false;
        bool isButtonsOnScreen = false;
        bool isButtonsGenerated = false;
        bool nemvalasztottszintet = false;
        bool ujjatekinditas = true;
        bool jobbklikk = false;
        string egy = "1";
        string ketto = "2";
        string harom = "3";
        string negy ="4";
        string ot= "5";
        string hat= "6";
        string het= "7";
        string nyolc= "8";
        string kilenc= "9";

        public Sudoku_game(int id,string felhasznalonev)
        {
            ConnectionString =
                @"Server   = (localdb)\MSSQLLocalDB;" +
                 "Database = szakdolgozat;";
            InitializeComponent();
            Felhasznalonev = felhasznalonev;
            Id = id;
        }
        //var wrongCells = new List<SudokuCell>();

        private void createCells()
        {
            if (!isButtonsOnScreen)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        cells[i, j] = new SudokuCell();
                        cells[i, j].Name = "Cell" + (i * 9 + j).ToString();
                        cells[i, j].Width = WIDTH;
                        cells[i, j].Height = HEIGHT;
                        cells[i, j].Click += cell_Click;
                        //cells[i, j].MouseRightButtonDown += cell_RightClick;
                        cells[i, j].IsHitTestVisible = true;
                        cells[i, j].Focusable = true;
                        cells[i, j].Background = ((i / 3) + (j / 3)) % 2 == 0 ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.White);
                        cells[i, j].Foreground = new SolidColorBrush(Colors.DarkMagenta);
                        cells[i, j].Value = 0;
                        cells[i, j].FontSize = 14;
                        //cells[i, j].BorderBrush = new SolidColorBrush(Colors.Black);
                        Canvas.SetLeft(cells[i, j], 20 + STEP * currentX);
                        Canvas.SetTop(cells[i, j], 20 + STEP * currentY);
                        currentX += WIDTH;
                        MainCanvas.Children.Add(cells[i, j]);
                    }
                    currentX = 0;
                    currentY += HEIGHT;
                    isButtonsOnScreen = true;
                }
                /*for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        labelCells[i, j] = new LabelCell();
                        labelCells[i, j].Name = "Cell" + (i * 9 + j).ToString();
                        labelCells[i, j].Width = WIDTH;
                        labelCells[i, j].Content = $" {egy}  {ketto}  {harom}\n {negy}  {ot}  {hat}\n {het}  {nyolc}  {kilenc}";
                        labelCells[i, j].Height = HEIGHT;
                        //labelCells[i, j].MouseRightButtonDown += cell_RightClick;
                        labelCells[i, j].IsHitTestVisible = false;
                        labelCells[i, j].Focusable = false;
                        labelCells[i, j].Value = 0;
                        labelCells[i, j].FontSize = 8;
                        labelCells[i, j].Visibility = Visibility.Visible;
                        labelCells[i, j].Foreground = new SolidColorBrush(Colors.Blue);
                        //cells[i, j].BorderBrush = new SolidColorBrush(Colors.Black);
                        Canvas.SetLeft(labelCells[i, j], 20 + STEP * currentX_X);
                        Canvas.SetTop(labelCells[i, j], 15 + STEP * currentY_Y);
                        currentX_X += WIDTH;
                        MainCanvas.Children.Add(labelCells[i, j]);
                    }
                    currentX_X = 0;
                    currentY_Y += HEIGHT;
                    isButtonsOnScreen = true;

                }*/
            }
            else
            {
                foreach (var item in cells)
                {
                    MainCanvas.Children.Remove(item);
                    // Clear the cell only if it is not locked
                    if (item.IsLocked == false)
                    {
                        item.Clear();
                        item.Value = 0;
                    }
                }
                /*foreach (var item in labelCells)
                {
                    MainCanvas.Children.Remove(item);
                    if (item.IsLocked == false)
                    {
                        item.Clear();
                        item.Value = 0;
                    }
                }*/
                isButtonsOnScreen = false;
                currentX = 0;
                currentY = 0;
                currentX_X = 0;
                currentY_Y = 0;

                createCells();
            }
        }
        private void Numpad_Click(object sender, RoutedEventArgs e)
        {
            NumpadCell = sender as SudokuCell;
            SelectedCell.Content = NumpadCell.Content;
            SelectedCell.Value = NumpadCell.Value;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Numpads[i, j].Visibility = Visibility.Hidden;
                }
            }

            for (int k = 0; k < 9; k++)
            {
                for (int l = 0; l < 9; l++)
                {
                    cells[k, l].Background = ((k / 3) + (l / 3)) % 2 == 0 ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.White);
                }
            }

            newBtn.Visibility = Visibility.Hidden;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SelectedCell.Content = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Numpads[i, j].Visibility = Visibility.Hidden;
                }
            }

            for (int k = 0; k < 9; k++)
            {
                for (int l = 0; l < 9; l++)
                {
                    cells[k, l].Background = ((k / 3) + (l / 3)) % 2 == 0 ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.White);
                }
            }

            newBtn.Visibility = Visibility.Hidden;
            SelectedCell.Value = 0;
        }
        

        private void Delete_Hint_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void cell_Click(object sender, EventArgs e)
        {
            SelectedCell = sender as SudokuCell;

            if (SelectedCell.IsLocked)
                return;
            SelectedCell.Background = new SolidColorBrush(Colors.LightBlue);
            //MessageBox.Show(""+SelectedCell.Value);

            currentX = 382;
            currentY = 140;
            if (!isButtonsGenerated)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Numpads[i, j] = new SudokuCell();
                        Numpads[i, j].Name = "Numpad" + i.ToString();
                        Numpads[i, j].Width = WIDTH;
                        Numpads[i, j].Height = HEIGHT;
                        Numpads[i, j].Click += Numpad_Click;
                        Numpads[i, j].Content = i * 3 + j + 1;
                        //Numpads[i, j].Content = i * 3 + j + 1;
                        Numpads[i, j].Value = i * 3 + j + 1;
                        Numpads[i, j].Style = Application.Current.Resources["RoundButtonTemplate"] as Style;
                        Numpads[i, j].Background = new SolidColorBrush(Colors.DodgerBlue);
                        Numpads[i, j].MouseEnter += mouseEnterEvent;
                        Numpads[i, j].MouseLeave += mouseLeaveEvent;
                        Canvas.SetLeft(Numpads[i, j], STEP * currentX + 2);
                        Canvas.SetBottom(Numpads[i, j], STEP * currentY);
                        currentX += WIDTH;
                        MainCanvas.Children.Add(Numpads[i, j]);
                    }
                    currentX = 382;
                    currentY += HEIGHT;
                    isButtonsOnScreen = true;
                }
                if(Nyelv == 1)
                {
                    newBtn.Content = "Törlés";
                }
                else if(Nyelv == 2)
                {
                    newBtn.Content = "Delete";
                }
                newBtn.Name = "BT_Delete";
                newBtn.Width = 3 * WIDTH;
                newBtn.Height = HEIGHT;
                newBtn.Click += Delete_Click;
                newBtn.Style = Application.Current.Resources["RoundButtonTemplate"] as Style;
                newBtn.Background = new SolidColorBrush(Colors.DodgerBlue);
                newBtn.MouseEnter += DelmouseEnterEvent;
                newBtn.MouseLeave += DelmouseLeaveEvent;
                Canvas.SetLeft(newBtn, STEP * currentX + 2);
                Canvas.SetBottom(newBtn, STEP * currentY);
                MainCanvas.Children.Add(newBtn);

                isButtonsGenerated = true;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Numpads[i, j].Visibility = Visibility.Visible;
                    }
                }
                newBtn.Visibility = Visibility.Visible;
            }
        }

        

        private bool checking()
        {
            for (int i = 0; i < 9; i++)
            {
                List<int> Lista = new List<int>();

                for (int j = 0; j < 9; j++)
                {
                    if (!Lista.Contains((int)cells[i, j].Value))
                    {
                        Lista.Add((int)cells[i, j].Value);
                    }
                }
                if (Lista.Count != 9 || Lista.Contains(0))
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                List<int> Lista = new List<int>();

                for (int j = 0; j < 9; j++)
                {
                    if (!Lista.Contains((int)cells[j, i].Value))
                    {
                        Lista.Add((int)cells[j, i].Value);
                    }
                }
                if (Lista.Count != 9 || Lista.Contains(0))
                {
                    return false;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    List<int> Lista = new List<int>();
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            if (!Lista.Contains((int)cells[i * 3 + k, j * 3 + l].Value))
                            {
                                Lista.Add((int)cells[i * 3 + k, j * 3 + l].Value);
                            }
                        }
                    }
                    if (Lista.Count != 9 || Lista.Contains(0))
                    {
                        return false;
                    }
                }
            }
            return true;

        }


        private void startNewGame()
        {
            BorderPicture.Visibility = Visibility.Visible;
            loadValues();

            if (!nemvalasztottszintet)
            {
                MessageBox.Show("Válassz nehézségi szintet mielött játszanál!", "Hiba!");
            }
            else
            {
                showRandomValuesHints(hintsCount);
                if (RB_Easy.IsChecked == true)
                {
                    nehezseg = 0;
                }
                if (RB_Medium.IsChecked == true)
                {
                    nehezseg = 1;
                }
                if (RB_Hard.IsChecked == true)
                {
                    nehezseg = 2;
                }
            }

        }

        private void loadValues()
        {
            // Töröljük az egyes cellák értékeit
            foreach (var cell in cells)
            {
                cell.Clear();
                cell.Value = 0;
            }

            // Ezt a metódust rekurzívan fogjuk meghívni 
            // amíg nem talál megfelelő értékeket az egyes cellákhoz
            findValueForNextCell(0, -1);
        }

        private bool findValueForNextCell(int i, int j)
        {
            // Az i és j értékek növelése a következő cellára lépéshez
            // és ha az oszlopok véget érnek, lépjünk a következő sorba.
            if (++j > 8)
            {
                j = 0;

                // Exit if the line ends
                if (++i > 8)
                    return true;
            }

            var value = 0;
            var numsLeft = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Keressünk egy véletlenszerű és érvényes számot a cellához, és lépjünk a következő cellához. 
            // és ellenőrizze, hogy ki lehet-e osztani egy másik véletlenszerű és érvényes számmal.
            do
            {
                // Ha nem maradt szám a listában, akkor próbáljuk meg a következőt, 
                // visszatérünk az előző cellához, és egy másik számot rendelünk hozzá.
                if (numsLeft.Count < 1)
                {
                    cells[i, j].Value = 0;
                    return false;
                }

                // Vegyünk fel egy véletlen számot a listában maradt számok közül.
                value = numsLeft[rnd.Next(0, numsLeft.Count)];
                cells[i, j].Value = value;

                // Remove the allocated value from the list
                numsLeft.Remove(value);
            }
            while (!isValidNumber(value, i, j) || !findValueForNextCell(i, j));



            // Tesztelésből feltölteni az egészet.
            //cells[i, j].Content = value.ToString();

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


        private void showRandomValuesHints(int hintsCount)
        {
            // Show value in random cells
            // The hints count is based on the level player choose
            for (int i = 0; i < hintsCount; i++)
            {
                var rX = rnd.Next(9);
                var rY = rnd.Next(9);

                // Style the hint cells differently and
                // lock the cell so that player can't edit the value
                cells[rX, rY].Content = cells[rX, rY].Value.ToString();
                cells[rX, rY].Foreground = new SolidColorBrush(Colors.Black);
                cells[rX, rY].IsLocked = true;
                

            }
            check_valid_value();
        }

        private void check_valid_value()
        {
            for (int k = 0; k < 9; k++)
            {
                for (int l = 0; l < 9; l++)
                {
                    if ((string)cells[k, l].Content == string.Empty)
                    {
                        cells[k, l].Value = 0;
                    }
                }
            }
        }

        private void BT_ujjatek_Click(object sender, RoutedEventArgs e)
        {
            createCells();
            startNewGame();
            DispatcherTimerSample();
        }

        public void DispatcherTimerSample()
        {
            
            timer.Interval = TimeSpan.FromSeconds(1);
            if (checking())
            {
                timer.Stop();
            }
            else
            {
                if (ujjatekinditas)
                {
                    timer.Tick += timer_Tick;
                    ujjatekinditas = false;
                }
                else
                {
                    timer.Stop();
                    second = 0;
                    minute = 0;
                }
                timer.Start();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {

            second++;
            if(second % 60 == 0)
            {
                second = 0;
                minute++;
            }
            lblTime.Content = $"{minute}:{second}";
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var r = new SqlCommand($"SELECT nyelv FROM jatekos WHERE felhasznalonev = '" + Felhasznalonev + "';", c).ExecuteReader();
                r.Read();
                Nyelv = Convert.ToInt32(r[0].ToString());



                if (Nyelv == 1)
                {
                    LB_koszontes.Content = $"Üdvözöllek {Felhasznalonev}!";
                    BT_ujjatek.Content = "Új Játék";
                    BT_beallitasok.Content = "Beállítások";
                    BT_ranglista.Content = "Ranglista";
                    BT_Finish.Content = "Befejezés";
                    RB_Easy.Content = "Könnyü";
                    RB_Medium.Content = "Közepes";
                    RB_Hard.Content = "Nehéz";
                    LB_Time.Content = "Idő:";
                }
                else if (Nyelv == 2)
                {
                    LB_koszontes.Content = $"Welcome {Felhasznalonev}!";
                    BT_ujjatek.Content = "New Game";
                    BT_beallitasok.Content = "Settings";
                    BT_ranglista.Content = "Leaderboard";
                    BT_Finish.Content = "End Game";
                    RB_Easy.Content = "Easy";
                    RB_Medium.Content = "Medium";
                    RB_Hard.Content = "Hard";
                    LB_Time.Content = "Time:";
                }
            }


            BorderPicture.Visibility = Visibility.Hidden;
        }

        private void BT_ranglista_Click(object sender, RoutedEventArgs e)
        {
            new Ranglistmenu(Nyelv).Show();
        }

        private void BT_beallitasok_Click(object sender, RoutedEventArgs e)
        {
            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();

                var r = new SqlCommand($"SELECT jatekosnev FROM jatekos WHERE felhasznalonev = '" + Felhasznalonev + "';", c).ExecuteReader();

                r.Read();
                Jatekosnev = $"{r[0]}";
            }
            new Beallitasokmenu(Id,Felhasznalonev, Jatekosnev,Nyelv).Show();
        }

        private void BT_ujjatek_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {

            if (checking())
            {
                int Befejezettido = minute * 60 + second;
                gamefinished = true;
                DispatcherTimerSample();
                using (var c = new SqlConnection(ConnectionString))
                {
                    c.Open();
                    var r = new SqlCommand($"SELECT ranglista.ido FROM ranglista WHERE jatekosid = '" + Id + "';", c).ExecuteReader();
                    //HA NINCS ÉRTÉK HIBA VAN (ÚJNÁL)
                    r.Read();
                    int rekord = int.Parse(r[0].ToString());
                    c.Close();
                    if (rekord > Befejezettido)
                    {
                        if (Nyelv == 1)
                        {
                            MessageBox.Show("Rekord idő lett, gratulálok!");
                        }
                        else if (Nyelv == 2)
                        {
                            MessageBox.Show("Record time, congratulations!");
                        }
                        c.Open();
                        new SqlCommand($"UPDATE ranglista SET ranglista.ido = '{Befejezettido}' WHERE jatekosid = {Id};", c).ExecuteNonQuery();
                        
                    }
                    else
                    {
                        if (Nyelv == 1)
                        {
                            MessageBox.Show("Sikeresen befejezted, de nem lett rekord idő!");
                        }
                        else if (Nyelv == 2)
                        {
                            MessageBox.Show("You finished successfully, but no record time!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Rossz a megoldás!");
            }

        }

        private void CB_Checker_Check(object sender, RoutedEventArgs e)
        {
            if (RB_Easy.IsChecked == true)
            {
                RB_Medium.IsChecked = false;
                RB_Hard.IsChecked = false;
                nemvalasztottszintet = true;
                hintsCount = 200;
            }
            if (RB_Medium.IsChecked == true)
            {
                RB_Easy.IsChecked = false;
                RB_Hard.IsChecked = false;
                nemvalasztottszintet = true;
                hintsCount = 75;
            }
            if (RB_Hard.IsChecked == true)
            {
                RB_Easy.IsChecked = false;
                RB_Medium.IsChecked = false;
                nemvalasztottszintet = true;
                hintsCount = 15;
            }
        }

        private void BT_ujjatek_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Jobbklikk");
        }
        

        private void BT_ujjatek_MouseEnter(object sender, MouseEventArgs e)
        {
            BT_ujjatek.Background = new SolidColorBrush(Colors.Blue);
        }

        private void BT_ujjatek_MouseLeave(object sender, MouseEventArgs e)
        {
            BT_ujjatek.Background = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void BT_ranglista_MouseEnter(object sender, MouseEventArgs e)
        {
            BT_ranglista.Background = new SolidColorBrush(Colors.Blue);
        }

        private void BT_ranglista_MouseLeave(object sender, MouseEventArgs e)
        {
            BT_ranglista.Background = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void BT_beallitasok_MouseEnter(object sender, MouseEventArgs e)
        {
            BT_beallitasok.Background = new SolidColorBrush(Colors.Blue);
        }

        private void BT_beallitasok_MouseLeave(object sender, MouseEventArgs e)
        {
            BT_beallitasok.Background = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void BT_Finish_MouseEnter(object sender, MouseEventArgs e)
        {
            BT_Finish.Background = new SolidColorBrush(Colors.Blue);
        }

        private void BT_Finish_MouseLeave(object sender, MouseEventArgs e)
        {
            BT_Finish.Background = new SolidColorBrush(Colors.DodgerBlue);
        }
        private void mouseLeaveEvent(object sender, MouseEventArgs e)
        {
            NumpadCell = sender as SudokuCell;
            NumpadCell.Background = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void mouseEnterEvent(object sender, MouseEventArgs e)
        {
            NumpadCell = sender as SudokuCell;
            NumpadCell.Background = new SolidColorBrush(Colors.Blue);
        }

        private void DelmouseLeaveEvent(object sender, MouseEventArgs e)
        {
            newBtn.Background = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void DelmouseEnterEvent(object sender, MouseEventArgs e)
        {
            newBtn.Background= new SolidColorBrush(Colors.Blue);
        }
    }
}
