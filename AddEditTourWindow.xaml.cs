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
        private AgencyModel model = new AgencyModel();
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
                if (tour.ID != 0)
                {
                    if (TourRepository.EditTour())
                    {
                        MessageBox.Show("Изменение данных прошло успешно");
                        log = $"Добавление-Изменение тура | Успешное изменение тура №{tour.ID}";
                        Logger.Log(log);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Изменение данных не пройдено");
                        log = $"Добавление-Изменение тура | Ошибка при изменении тура №{tour.ID}";
                        Logger.Log(log);
                    }
                }
                else
                {
                    tour.State = cmbStatus.SelectedIndex + 1;
                    tour.Type = cmbType.SelectedIndex + 1;
                    tour.IsExists = 1;
                    if (TourRepository.AddTour(tour))
                    {
                        MessageBox.Show("Добавление данных прошло успешно");
                        log = $"Добавление-Изменение тура | Успешное добавление тура №{tour.ID}";
                        Logger.Log(log);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Добавление данных не пройдено");
                        log = $"Добавление-Изменение тура | Ошибка при добавлении тура №{tour.ID}";
                        Logger.Log(log);
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
            if (string.IsNullOrEmpty(tbCity.Text))
            {
                errors.AppendLine("Введите город путевки");
            }
            if (string.IsNullOrEmpty(tbCountry.Text))
            {
                errors.AppendLine("Введите страну путевки");
            }
            if (string.IsNullOrEmpty(tbPrice.Text))
            {
                errors.AppendLine("Введите цену за билет");
            }
            if (dpDeparture.SelectedDate == null)
            {
                errors.AppendLine("Выберите дату отправки");
            }
            if (dpArrival.SelectedDate == null)
            {
                errors.AppendLine("Выберите дату отправки");
            }
            if (cmbStatus.SelectedItem == null)
            {
                errors.AppendLine("Выберите статус путевки");
            }
            if (cmbType.SelectedItem == null)
            {
                errors.AppendLine("Выберите тип путевки");
            }
            if (string.IsNullOrEmpty(tbTickets.Text))
            {
                errors.AppendLine("Введите количество билетов");
            }
            if (dpDeparture.SelectedDate > dpArrival.SelectedDate)
            {
                errors.AppendLine("Дата отправки не может быть больше даты прибытия");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                log = $"Добавление-Изменение тура | Не пройдена валидация";
                Logger.Log(log);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
