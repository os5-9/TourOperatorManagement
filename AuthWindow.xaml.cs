using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private AgencyModel model = new AgencyModel();
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
