using System;
using System.Windows;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private AgencyModel model = new AgencyModel();
        public static string log;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow auth = new AuthWindow();
            auth.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFullName.Text) && !string.IsNullOrEmpty(tbLogin.Text) && !string.IsNullOrEmpty(tbPassword.Text) && !string.IsNullOrEmpty(tbComment.Text))
            {
                Operators anOperator = new Operators();
                anOperator.FullName = tbFullName.Text;
                anOperator.Login = tbLogin.Text;
                anOperator.Password = tbPassword.Text;
                anOperator.Comment = tbComment.Text;
                anOperator.IsDenied = 0;
                model.Operators.Add(anOperator);
                try
                {
                    model.SaveChanges();
                    MessageBox.Show("Ваш аккаунт зарегистрирован и отправлен на верификацию, это может занять несколько дней");
                    log = $"Регистрация | Запрос на верификацию оператора {anOperator.FullName}\n";
                    Logger.Log(log);
                    btnCancel_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
                log = $"Регистрация | Пустые поля\n";
                Logger.Log(log);
            }
        }
    }
}
