using System.Windows;
using System.Windows.Controls;
using SudokuRep;
using static SudokuWPF.Security;

namespace SudokuWPF
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private AuthorizationWindow _authorizationWindow;        
        public LoginPage(AuthorizationWindow window)
        {
            InitializeComponent();
            _authorizationWindow = window;            
        }

        private void log_in_click(object sender, RoutedEventArgs e)
        {
            if (Check_Fields())
            {
                string userPassword = ((_authorizationWindow.Owner as MainWindow).DataContext as SudokuModel).GetUserPassByLogin(TextboxLogin.Text);
                if (userPassword != null)
                {
                    if (VerifyHashedPassword(userPassword, PasswordBox.Password))
                    {                                                                       
                        ((_authorizationWindow.Owner as MainWindow).DataContext as SudokuModel).AuthUser = ((_authorizationWindow.Owner as MainWindow).DataContext as SudokuModel).GetUserByCredentials(TextboxLogin.Text, userPassword);
                        _authorizationWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароль введен неверно!", "Некорректные данные", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Данный пользователь не зарегистрирован!", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }                
                
        }        
        private bool Check_Fields()
        {

            if (!(Checker.check_text(TextboxLogin.Text, "Введите имя и фамилию!")))
                return false;
            if (!(Checker.check_text(PasswordBox.Password, "Введите пароль!")))
                return false;
            return true;
        }

      
        private void Sign_up_click(object sender, RoutedEventArgs e)
        {
            _authorizationWindow.AuthFrame.Content = new RegistrationPage(_authorizationWindow);
        }
    }
}

