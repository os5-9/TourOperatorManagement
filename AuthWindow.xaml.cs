using System.Linq;
using System.Windows;
using System.Windows.Input;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private AgencyModel model = new AgencyModel();
        public static string log;
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLogin.Text) && !string.IsNullOrEmpty(tbPassword.Password))
            {
                string login = tbLogin.Text;
                string password = tbPassword.Password;
                if (model.Operators.Where(x => x.Password == password && x.Login == login).Any())
                {
                    var person = model.Operators.Where(x => x.Password == password && x.Login == login).FirstOrDefault();
                    if (person.IsDenied == 1)
                    {
                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                        log = $"Авторизация | Вход оператора {person.FullName}";
                        Logger.Log(log);
                    }
                    else
                    {
                        MessageBox.Show("Ваш аккаунт на верификации, попробуйте позже");
                        log = $"Авторизация | Попытка входа в неверифицированный аккаунт";
                        Logger.Log(log);
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                    log = $"Авторизация | Неверный логин или пароль";
                    Logger.Log(log);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
                log = $"Авторизация | Пустые поля логина и пароля";
                Logger.Log(log);
            }
        }

        private void tbGoToRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnEnter_Click(sender, e);
            }
        }
    }
}
