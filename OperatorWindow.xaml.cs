using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private IEnumerable<Tours> allTours;
        private static string log;

        public OperatorWindow()
        {
            InitializeComponent();

            allTours = TourRepository.GetAllTours();
            cmbStatus.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            UpdateTours();
            log = $"Главное окно оператора | Просмотр данных";
            Logger.Log(log);
        }

        private void UpdateTours()
        {
            dgTour.ItemsSource = allTours.ToList();
        }
        private void SearchTours()
        {
            allTours = TourRepository.GetAllTours();
            if ((cmbStatus.SelectedItem != null) && (cmbType.SelectedItem != null))
            {
                string status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();
                string type = ((ComboBoxItem)cmbType.SelectedItem).Content.ToString();
                if (!string.IsNullOrEmpty(tbCity.Text))
                {
                    allTours = TourRepository.SearchTourCity(allTours, tbCity.Text);
                }
                if (!string.IsNullOrEmpty(tbCountry.Text))
                {
                    allTours = TourRepository.SearchTourCountry(allTours, tbCountry.Text);
                }
                if (cmbStatus.SelectedIndex != 0)
                {
                    allTours = TourRepository.SearchTourStatus(allTours, status);
                }
                if (cmbType.SelectedIndex != 0)
                {
                    allTours = TourRepository.SearchTourType(allTours, type);
                }
                if (!string.IsNullOrEmpty(tbPrice.Text))
                {
                    allTours = TourRepository.SearchTourPrice(allTours, int.Parse(tbPrice.Text.ToString()));
                }
                if ((dpArrivalS.SelectedDate != null) && (dpArrivalF.SelectedDate != null))
                {
                    var arrivalS = dpArrivalS.SelectedDate.Value;
                    var arrivalF = dpArrivalF.SelectedDate.Value;
                    allTours = TourRepository.SearchTourarrivalal(allTours, arrivalS, arrivalF);
                }
                if ((dpDepartureS.SelectedDate != null) && (dpDepartureF.SelectedDate != null))
                {
                    var departureS = dpDepartureS.SelectedDate.Value;
                    var departureF = dpDepartureF.SelectedDate.Value;
                    allTours = TourRepository.SearchTourdepartureture(allTours, departureS, departureF);
                }
            }
            UpdateTours();
        }
        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTours();
        }
        private void tbCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTours();
        }
        private void tbPrice_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditTourWindow window = new AddEditTourWindow(null);
            window.Show();
            log = $"Главное окно оператора | Переход к добавлению тура";
            Logger.Log(log);
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Tours)dgTour.SelectedItem;
            if (selected != null)
            {
                Tours tour = TourRepository.GetToutByID(selected.ID);
                AddEditTourWindow window = new AddEditTourWindow(tour);
                window.Show();
                log = $"Главное окно оператора | Переход к редактированию тура №{tour.ID}";
                Logger.Log(log);
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите тур для редактирования");
                log = $"Главное окно оператора | Нажатие на редактирование без выбора строки";
                Logger.Log(log);
            }
        }
    }
}
