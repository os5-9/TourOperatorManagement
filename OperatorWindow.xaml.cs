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
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private IEnumerable<Tours> allTours;

        public OperatorWindow()
        {
            InitializeComponent();

            allTours = TourRepository.GetAllTours();
            cmbStatus.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            UpdateTours();
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

    }
}
