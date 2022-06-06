using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private IEnumerable<Tours> allTours;
        private int countTours = 18;
        private int page = 1;
        private int maxPage;
        private static Agency model = new Agency();

        public OperatorWindow()
        {
            InitializeComponent();
            dpArrivalS.DisplayDateStart = DateTime.Now;
            dpArrivalF.DisplayDateStart = DateTime.Now;
            dpDepartureS.DisplayDateStart = DateTime.Now;
            dpDepartureF.DisplayDateStart = DateTime.Now;

            allTours = model.Tours.Where(x => x.IsExists == 1);
            // TourRepository.GetAllTours();
            cmbStatus.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;
            UpdateTours();
        }

        private void UpdateTours()
        {
            List<Tours> listTours = allTours.ToList();
            List<Tours> listInPage = listTours.Skip((page - 1) * countTours).Take(countTours).ToList();
            maxPage = (int)Math.Ceiling(listTours.Count * 1.0 / countTours);
            tbCurrentPage.Content = page.ToString();
            tbTotalPages.Content = $" из {maxPage}";
            dgTour.ItemsSource = listInPage;
        }

        private void GoToFirst(object sender, RoutedEventArgs e)
        {
            page = 1;
            UpdateTours();
        }
        private void GoToPreviors(object sender, RoutedEventArgs e)
        {
            if (page > 1)
            {
                page--;
            }
            UpdateTours();
        }
        private void GoToNext(object sender, RoutedEventArgs e)
        {
            if (page < maxPage)
            {
                page++;
            }
            UpdateTours();
        }
        private void GoToLast(object sender, RoutedEventArgs e)
        {
            page = maxPage;
            UpdateTours();
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
            Sort();
            UpdateTours();
        }
        private void Sort()
        {
            int index = cmbSort.SelectedIndex;
            allTours = allTours.OrderBy(t => t.ID).ToList();
            switch (index)
            {
                case 0:
                    allTours = allTours.OrderBy(t => t.ID).ToList();
                    break;
                case 1:
                    allTours = allTours.OrderBy(t => t.Price).ToList();
                    break;
                case 2:
                    allTours = allTours.OrderByDescending(t => t.Price).ToList();
                    break;
                case 3:
                    allTours = allTours.OrderBy(t => t.City).ToList();
                    break;
                case 4:
                    allTours = allTours.OrderByDescending(t => t.City).ToList();
                    break;
                case 5:
                    allTours = allTours.OrderBy(t => t.Country).ToList();
                    break;
                case 6:
                    allTours = allTours.OrderByDescending(t => t.Country).ToList();
                    break;
                case 7:
                    allTours = allTours.OrderBy(t => t.Departure).ToList();
                    break;
                case 8:
                    allTours = allTours.OrderByDescending(t => t.Departure).ToList();
                    break;
                case 9:
                    allTours = allTours.OrderBy(t => t.Arrival).ToList();
                    break;
                case 10:
                    allTours = allTours.OrderByDescending(t => t.Arrival).ToList();
                    break;
            }
        }
        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTours();
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
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите тур для редактирования");
            }
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Tours)dgTour.SelectedItem;
            if (selected != null)
            {
                if (MessageBox.Show("Вы уверенны, что хотите снять с продажи выбранный тур?\nДанное действие нельзя будет отменить", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TourRepository.RemoveFromSale(selected.ID);
                    UpdateTours();
                }
            }
            else
            {
                MessageBox.Show("Выберите тур для снятия с продажи");
            }
            SearchTours();
        }
        private void dgTour_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selected = (Tours)dgTour.SelectedItem;
            if (selected != null)
            {
                Tours tour = TourRepository.GetToutByID(selected.ID);
                AddEditTourWindow window = new AddEditTourWindow(tour);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите тур для редактирования");
            }
            UpdateTours();
        }
    }
}
