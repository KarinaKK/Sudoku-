using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SudokuRep;

namespace SudokuWPF
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private MainWindow _mainWindow;
       
        public GamePage(MainWindow window,int level)
        {
            _mainWindow = window;            
            InitializeComponent();
            GenerateGrid();
            ContainGrid();
            LevelLabel.Content += " " + level;
            
        }
        private TextBox[,] sudokuTextBlocks;

        private void GenerateGrid()
        {
            
            sudokuTextBlocks = new TextBox[9, 9];
            for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
            {

                var border = new Border
                {
                    BorderBrush = Brushes.Red,
                    BorderThickness = GetThickness(i, j, 0.1, 0.3)
                };
                sudokuTextBlocks[i, j] = new TextBox
                {
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center
                    
                };
                sudokuTextBlocks[i, j].KeyDown += TextBox_KeyDown;
                border.SizeChanged += Border_SizeChanged;
                border.Child = sudokuTextBlocks[i, j];             
                Grid.SetRow(border, i);
                Grid.SetColumn(border, j);
                SudokuGrid.Children.Add(border);                    
            }
        }

        
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Regex regex=new Regex("[D]{1}[1-9]");
            bool isDigit = regex.IsMatch(e.Key.ToString());
            if (!isDigit || ((TextBox)sender).Text.Length >= 1)
            {
                e.Handled = true;
            }
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((sender as Border).Child as TextBox).Height = (sender as Border).ActualHeight;
            ((sender as Border).Child as TextBox).Width = (sender as Border).ActualWidth;
        }

        private void ContainGrid()
        {                      
            for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                sudokuTextBlocks[i, j].SetBinding(TextBox.TextProperty, new Binding($"GameBoard[{i}][{j}]"));            
            DataContext = _mainWindow.DataContext;
            SealBoard();
        }

        private void SealBoard()
        {
            for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if ((DataContext as SudokuModel).GameBoard[i][j]!=null)
                    sudokuTextBlocks[i, j].IsReadOnly = true;
        }
        private Thickness GetThickness(int i, int j, double thin, double thick)
        {
            var top = i % 3 == 0 ? thick : thin;
            var bottom = i % 3 == 2 ? thick : thin;
            var left = j % 3 == 0 ? thick : thin;
            var right = j % 3 == 2 ? thick : thin;
            return new Thickness(left, top, right, bottom);
        }
        private void CheckButton_OnClick(object sender, RoutedEventArgs e)
        {            
            bool res = true;
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                    if ((DataContext as SudokuModel).GameBoard[i][j] !=
                        (DataContext as SudokuModel).GameBoardFull[i][j])
                    {
                        res = false;
                        break;
                    }
                if(res==false)
                    break;                
            }
            if(res)
                MessageBox.Show("Судоку решен неверно", "\tПроверка", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                MessageBox.Show("Судоку решен верно", "\tПоздравляем", MessageBoxButton.OK, MessageBoxImage.Information);
                (DataContext as SudokuModel).FinishGame();         
                clearGrid();
            }

        }

        private void clearGrid()
        {
            foreach (var textBox in sudokuTextBlocks)
            {
                textBox.IsReadOnly = true;
                textBox.Clear();
            }
        }
    }
}
