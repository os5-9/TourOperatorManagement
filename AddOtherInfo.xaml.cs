using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TourOperatorManagement.Models;
using Word = Microsoft.Office.Interop.Word;

namespace TourOperatorManagement
{
    public partial class AddOtherInfo : Window
    {
        private List<Routes> routes = new List<Routes>();
        private Tours currentTour;
        public AddOtherInfo(Tours tour)
        {
            InitializeComponent();
            currentTour = tour;
            tbPriceDigit.Text = tour.Price.ToString();
        }

        private void btnAddRoute_Click(object sender, RoutedEventArgs e)
        {
            if (dpRoute.SelectedDate != null && tbRouteName.Text != "" && tbTime.Text != "__:__")
            {
                double minutes = double.Parse(tbTime.Text.Substring(3));
                double hours = double.Parse(tbTime.Text.Substring(0, 2));
                if (minutes <= 60 && hours < 24)
                {
                    if (minutes == 60 && hours == 24)
                    {
                        return;
                    }
                    DateTime dateTime = (DateTime)dpRoute.SelectedDate;
                    dateTime = dateTime.AddHours(hours);
                    dateTime = dateTime.AddMinutes(minutes);
                    Routes route = new Routes
                    {
                        RouteName = tbRouteName.Text,
                        ReisNumber = tbReisNumber.Text,
                        RouteDateTime = dateTime
                    };
                    routes.Add(route);
                    dgRoutes.ItemsSource = routes.ToList();
                }
            }
        }
        private void ControlForDigit(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
        private bool Validation()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(tbNameHotel.Text))
            {
                errors.AppendLine("Введите наименование средства размещения");
            }
            if (string.IsNullOrEmpty(tbAddressHotel.Text))
            {
                errors.AppendLine("Введите адрес средства размещения");
            }
            if (cmbCategoryHotel.SelectedItem == null)
            {
                errors.AppendLine("Выберите категорию средства размещения ");
            }
            if (dpGo.SelectedDate == null)
            {
                errors.AppendLine("Выберите дату заезда средства размещения");
            }
            if (dpOut.SelectedDate == null)
            {
                errors.AppendLine("Выберите дату выезда(выселения) средства размещения");
            }
            if (string.IsNullOrEmpty(tbPriceDigit.Text))
            {
                errors.AppendLine("Введите цену за билет цифрами");
            }
            if (string.IsNullOrEmpty(tbPriceLetters.Text))
            {
                errors.AppendLine("Введите цену за билет прописью");
            }
            if (routes.Count == 0)
            {
                errors.AppendLine("Введите информацию об услугах перевозки (Кнопка добавлить маршрут)");
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                var application = new Word.Application();
                Word.Document document = application.Documents.Open(Environment.CurrentDirectory + @"\Приложение1.docx");

                document.Bookmarks["NameHotel"].Select();
                application.Selection.TypeText($"{tbNameHotel.Text}");
                document.Bookmarks["AddressHotel"].Select();
                application.Selection.TypeText($"{tbAddressHotel.Text}");
                document.Bookmarks["CategoryHotel"].Select();
                application.Selection.TypeText($"{cmbCategoryHotel.Text}");
                document.Bookmarks["Go"].Select();
                application.Selection.TypeText($"{dpGo.SelectedDate:dd.MM.yyyy}");
                document.Bookmarks["Out"].Select();
                application.Selection.TypeText($"{dpOut.SelectedDate:dd.MM.yyyy}");
                document.Bookmarks["PriceDigit"].Select();
                application.Selection.TypeText($"{tbPriceDigit.Text}");
                document.Bookmarks["PriceLetters"].Select();
                application.Selection.TypeText($"{tbPriceLetters.Text}");

                var dataRange = document.Bookmarks["Routes"].Range;
                Word.Paragraph tableparag = document.Paragraphs.Add();
                Word.Range tablerange = dataRange;
                Word.Table RouteTable = document.Tables.Add(tablerange, routes.Count + 1, 3);
                RouteTable.Borders.InsideLineStyle = RouteTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                Word.Range cellrange;
                cellrange = RouteTable.Cell(1, 1).Range;
                cellrange.Text = "Маршрут";
                cellrange = RouteTable.Cell(1, 2).Range;
                cellrange.Text = "Номер рейса";
                cellrange = RouteTable.Cell(1, 3).Range;
                cellrange.Text = "Дата/время";
                RouteTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int j = 0;
                for (int i = 2; i < routes.Count + 2; i++)
                {
                    RouteTable.Cell(i, 1).Range.Text = routes[j].RouteName;
                    RouteTable.Cell(i, 2).Range.Text = routes[j].ReisNumber;
                    RouteTable.Cell(i, 3).Range.Text = routes[j].RouteDateTime.ToString();
                    j++;
                }
                application.Visible = true;

                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\Дополнительные условия"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\Дополнительные условия");
                }
                document.SaveAs2(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\Дополнительные условия\\Приложение 1 к туру №{ currentTour.ID}");
                currentTour.Attachment = $"Приложение 1 к туру №{currentTour.ID}.docx";
                TourRepository.EditTour();
                this.Close();
            }
        }
    }
}
