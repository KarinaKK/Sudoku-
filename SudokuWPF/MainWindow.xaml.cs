using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SudokuRep;

namespace SudokuWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            ((SudokuModel) DataContext).AuthChanged += on_AuthChanged;
        }

        private void on_AuthChanged(User user)
        {
            if (user == null)
            {
                LogInButton.Visibility = Visibility.Visible;
                LogInExpander.Visibility = Visibility.Hidden;
                AuthLabel.Visibility = Visibility.Visible;
                ButtonNg.IsEnabled = false;
                SettingsExpander.IsEnabled =false;
            }
            else
            {
                LogInExpander.Visibility = Visibility.Visible;
                LogInExpander.Header = user.login;
                LogInButton.Visibility = Visibility.Hidden;
                AuthLabel.Visibility = Visibility.Hidden;
                ButtonNg.IsEnabled=true;
                SettingsExpander.IsEnabled=true;
            }
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {        
            AuthorizationWindow p = new AuthorizationWindow { Owner = this };
            p.ShowDialog();
        }
       
        private void NewGame_OnClick(object sender, RoutedEventArgs e)
        {
            int level = 1;
            if (L2RadioButton.IsChecked != null)
                if((bool) L2RadioButton.IsChecked)
                level = 2;
            if (L3RadioButton.IsChecked != null)
                if((bool)L3RadioButton.IsChecked)
                level = 3;
           (DataContext as SudokuModel).StartGame(level);
            MainFrame.Content = new GamePage(this,level);
        }

        private void MainWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void OnLogOutClick(object sender, MouseButtonEventArgs e)
        {
            if(MainFrame.Content is GamePage)
                if (MessageBox.Show("Выйдя из системы до завершения игры, вы потеряете результат, продолжить?",
                        "\tСмена пользователя", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
           (DataContext as SudokuModel).AuthUser = null;
            MainFrame.Content = new LeadersPage(this);
                

        }

        private void OnExit(object sender, RoutedEventArgs e)
        {            
            Close();  
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите выйти?", "\tЗавершение работы", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content =new LeadersPage(this);
        }
    }
}
