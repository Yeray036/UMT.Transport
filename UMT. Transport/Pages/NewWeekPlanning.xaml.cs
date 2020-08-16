using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using UMT.Transport.Classes;

namespace UMT.Transport.Pages
{
    /// <summary>
    /// Interaction logic for NewWeekPlanning.xaml
    /// </summary>
    public partial class NewWeekPlanning : Page
    {
        List<PersonModel> employees = new List<PersonModel>();
        public static DateTime year;
        public static Calendar calendar;

        public NewWeekPlanning()
        {
            InitializeComponent();
            List<string> names = new List<string>();
            foreach (var item in SqliteHandler.LoadAllEmployeesOnName())
            {
                if (names.Contains(item))
                {
                    continue;
                }
                else
                {
                    names.Add(item);
                }
            }
            NameFieldComboBox.ItemsSource = names;
        }

        private async void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            calendar = sender as Calendar;

            if (calendar.SelectedDate.HasValue)
            {
                await Task.Run(() => Dispatcher.BeginInvoke(
                    new ThreadStart(async () => await LoadDataGridFromDate(calendar))));
            }
        }

        private async Task LoadDataGridFromDate(Calendar calendar)
        {
            if (calendar.SelectedDate.HasValue)
            {
                employees.Clear();
                DayDataGrid.ItemsSource = null;
                DateTime date = calendar.SelectedDate.Value;
                this.DatumInputBox.Text = $"{date.Day}-{date.Month}";
                year = calendar.SelectedDate.Value;
                employees = await Task.Run(() => SqliteHandler.LoadEmployeesOnDate($"{date.Day}-{date.Month}"));
                DayDataGrid.ItemsSource = employees;
            }
        }

        private void PlacePersNrInField(object sender, SelectionChangedEventArgs e)
        {
            if (LastNameFieldComboBox.SelectedItem != null)
            {
                var PersNr = SqliteHandler.LoadEmployeePersNr(LastNameFieldComboBox.SelectedValue.ToString());
                PersNrTextField.Text = PersNr[0];
            }
        }

        private void PlaceLastNameInField(object sender, SelectionChangedEventArgs e)
        {
            if (NameFieldComboBox.SelectedItem != null)
            {
                var LastName = SqliteHandler.LoadAllEmployeesOnLastName(NameFieldComboBox.SelectedValue.ToString());
                if (LastName.Count > 2)
                {
                    LastNameFieldComboBox.ItemsSource = LastName;
                }
                else
                {
                    LastNameFieldComboBox.ItemsSource = LastName;
                    LastNameFieldComboBox.Text = LastName[0];
                }
            }
        }

        private async void AddNewWorkDayBtn_Click(object sender, RoutedEventArgs e)
        {
            PersonModel employee = new PersonModel();

            employee.Datum = this.DatumInputBox.Text;
            employee.Jaar = $"{year.Year}";
            employee.Begin_tijd = this.BeginTimeFieldComboBox.Text;
            if (this.PersNrTextField.Text != String.Empty && employee.Begin_tijd != String.Empty)
            {
                employee.PersNr = int.Parse(this.PersNrTextField.Text);
            }
            else
            {
                employee.PersNr = -1;
            }
            if (employee.PersNr != -1 && employee.Datum != String.Empty)
            {
                SqliteHandler.SaveNewEmployeeWorkDay(employee);
                await Task.Run(() => Dispatcher.BeginInvoke(
                    new ThreadStart(async () => await LoadDataGridFromDate(calendar))));
                this.NameFieldComboBox.Text = "";
                this.LastNameFieldComboBox.Text = "";
                this.PersNrTextField.Text = "";
                this.BeginTimeFieldComboBox.Text = "";
                this.EndTimeFieldComboBox.Text = "";
            }
            else
            {
                MessageBox.Show("Geen geldige werkdag data ingevoerd.");
                return;
            }
        }
    }
}
