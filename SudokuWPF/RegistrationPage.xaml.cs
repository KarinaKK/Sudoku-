using System;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Controls;
using SudokuRep;

namespace SudokuWPF
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private AuthorizationWindow _authorizationWindow;
        

        public RegistrationPage(AuthorizationWindow authorizationWindow)
        {           
            InitializeComponent();
            _authorizationWindow = authorizationWindow;            
        }



        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            User user = new User();
            if (Check_Fields())
            {
                user.login = TextboxLogin.Text;
                user.password = Security.HashPassword(TextboxPassword.Password);
                if (!is_registred(user))
                {

                    //
                    //((_authorizationWindow.Owner as MainWindow).DataContext as Budget).Save_Data();
                    //(_authorizationWindow.Owner as MainWindow).GetCurrentUser = user;                   
                    ((_authorizationWindow.Owner as MainWindow).DataContext as SudokuModel).AddOrUpdateUser(user);
                    
                        MessageBox.Show("Вы успешно зарегистрированы!", "\tРегистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                    ((_authorizationWindow.Owner as MainWindow).DataContext as SudokuModel).AuthUser = user;
                    _authorizationWindow.Close();
                }
               
            }


        }

        private bool is_registred(User user)
        {
            if (((_authorizationWindow.Owner as MainWindow).DataContext as SudokuModel).CheckUserByName(user))
            {
                MessageBox.Show("Пользователь с таким никнеймом уже зарегистрирован!", "Некорректный ввод",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            return false;
        }
        private bool Check_Fields()
        {

            if (!(Checker.check_text(TextboxLogin.Text, "Введите Никнейм!")))
                return false;                       
            if (!(Checker.check_text(TextboxPassword.Password, "Введите пароль!")))
                return false;
            if (TextboxPassword.Password.Length < 8)
            {
                MessageBox.Show("Минимальная длина пароля 8 символов!", "Некорректный ввод",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!(Checker.check_text(TextboxPasswordConfirm.Password, "Подтвердите пароль!")))
                return false;
            if (TextboxPassword.Password != TextboxPasswordConfirm.Password)
            {
                MessageBox.Show("Введенные пароли не совпадают", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}

