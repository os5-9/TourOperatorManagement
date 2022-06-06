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
        private Agency model = new Agency();
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
            }
        }
    }
}
