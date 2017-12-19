using System.Windows;
using System.Windows.Controls;

namespace SudokuWPF
{
    /// <summary>
    /// Логика взаимодействия для LeadersPage.xaml
    /// </summary>
    public partial class LeadersPage : Page
    {
        private MainWindow _mainWindow;
        public LeadersPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            DataContext = _mainWindow.DataContext;           
            InitializeComponent();
        }

        private void LeadersPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as SudokuModel).UpdateRaiting();
        }
    }
}
