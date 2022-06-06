using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    /// <summary>
    /// Логика взаимодействия для AddEditTourWindow.xaml
    /// </summary>
    public partial class AddEditTourWindow : Window
    {
        private static string log;
        private Tours tour;

        public AddEditTourWindow(Tours currentTour)
        {
            InitializeComponent();
            dpDeparture.DisplayDateStart = DateTime.Now;
            dpArrival.DisplayDateStart = DateTime.Now;
            if (currentTour == null)
            {
                tour = new Tours();
                this.Title = "Добавление тура";
                tbTickets.Text = "0";
                tour.Tickets = 0;
            }
            else
            {
                tour = currentTour;
                cmbStatus.SelectedIndex = (int)tour.State - 1;
                cmbType.SelectedIndex = (int)tour.Type - 1;
                this.Title = $"Редактирование тура №{tour.ID}";
            }
            DataContext = tour;
        }

        private void ControlForDigit(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OperatorWindow window = new OperatorWindow();
            window.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                tour.State = cmbStatus.SelectedIndex + 1;
                tour.Type = cmbType.SelectedIndex + 1;
                tour.IsExists = 1;
                tour.Tickets = int.Parse(tbTickets.Text);
                if (tour.ID != 0)
                {
                    if (TourRepository.EditTour())
                    {
                        MessageBox.Show("Изменение данных прошло успешно");
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Изменение данных не пройдено");
                    }
                }
                else
                {
                    tour.IsApproved = 0;
                    if (TourRepository.AddTour(tour))
                    {
                        MessageBox.Show("Добавление данных прошло успешно");
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Добавление данных не пройдено");
                    }
                }
            }
            else
            {
                return;
            }
        }

        private bool Validation()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(tbCity.Text))
            {
                errors.AppendLine("Введите город путевки");
            }
            if (string.IsNullOrWhiteSpace(tbCountry.Text))
            {
                errors.AppendLine("Введите страну путевки");
            }
            if (string.IsNullOrWhiteSpace(tbPrice.Text))
            {
                errors.AppendLine("Введите цену за билет");
            }
            if (dpDeparture.SelectedDate == null)
            {
                errors.AppendLine("Выберите дату окончания тура");
            }
            if (dpArrival.SelectedDate == null)
            {
                errors.AppendLine("Выберите дату начала тура");
            }
            if (cmbStatus.SelectedItem == null)
            {
                errors.AppendLine("Выберите статус путевки");
            }
            if (cmbType.SelectedItem == null)
            {
                errors.AppendLine("Выберите тип путевки");
            }
            if (string.IsNullOrWhiteSpace(tbTickets.Text))
            {
                errors.AppendLine("Введите количество билетов");
            }
            if (dpDeparture.SelectedDate > dpArrival.SelectedDate)
            {
                errors.AppendLine("Дата отправки не может быть больше даты прибытия");
            }
            if (tbTickets.Text == "0")
            {
                errors.AppendLine("Введите количество билетов");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTickets.Text))
            {
                int tickets = int.Parse(tbTickets.Text);
                if (tickets == 1)
                {
                    btnMinus.IsEnabled = false;
                    tbTickets.Text = (tickets - 1).ToString();
                    return;
                }
                if (tickets > 0)
                {
                    btnMinus.IsEnabled = true;
                    tbTickets.Text = (tickets - 1).ToString();
                }
            }
            else
            {
                tbTickets.Text = "0";
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTickets.Text))
            {
                int tickets = int.Parse(tbTickets.Text);
                btnMinus.IsEnabled = true;
                tbTickets.Text = (tickets + 1).ToString();
            }
            else
            {
                tbTickets.Text = "1";
            }
        }
    }
}
