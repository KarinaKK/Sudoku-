using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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
        private TextBlock[,] sudokuTextBlocks;

        private void GenerateGrid()
        {
            
            sudokuTextBlocks = new TextBlock[9, 9];
            for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
            {

                var border = new Border
                {
                    BorderBrush = Brushes.Red,
                    BorderThickness = GetThickness(i, j, 0.1, 0.3)
                };
                sudokuTextBlocks[i, j] = new TextBlock
                {
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                border.Child = sudokuTextBlocks[i, j];
                Grid.SetRow(border, i);
                Grid.SetColumn(border, j);
                SudokuGrid.Children.Add(border);
            }
        }
        private void ContainGrid()
        {                      
            for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                sudokuTextBlocks[i, j].SetBinding(TextBlock.TextProperty, new Binding($"GameBoard[{i}][{j}]"));            
            DataContext = _mainWindow.DataContext;
            SealBoard();
        }

        private void SealBoard()
        {
            for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (sudokuTextBlocks[i, j].Text != null)
                    sudokuTextBlocks[i, j].IsEnabled = false;
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
            if(!res)
                MessageBox.Show("Судоку решен неверно", "\tПроверка", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                MessageBox.Show("Судоку решен верно", "\tПоздравляем", MessageBoxButton.OK, MessageBoxImage.Information);
                (DataContext as SudokuModel).NewGame.FinishTime = DateTime.Now;
                (DataContext as SudokuModel).AddUserData();
            }

        }
    }
}
