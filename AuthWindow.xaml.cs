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
        private Agency model = new Agency();
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
                       
                        OperatorWindow window = new OperatorWindow();
                        window.Show();
                        this.Close(); 
                    }
                    if (person.IsDenied == 2)
                    {
                        MessageBox.Show("Ваш аккаунт в качестве оператора отклонен");
                    }
                    else
                    {
                        MessageBox.Show("Ваш аккаунт на верификации, попробуйте позже");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
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
