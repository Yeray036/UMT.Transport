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
using UMT.Transport.UserControls;

namespace UMT.Transport.Pages
{
    /// <summary>
    /// Interaction logic for NewWeekPlanning.xaml
    /// </summary>
    public partial class NewWeekPlanning : Page
    {
        List<PersonModel> employees = new List<PersonModel>();
        List<AllEmployeesPerDepot> AllEmployees = new List<AllEmployeesPerDepot>();
        public static DateTime year;
        public static Calendar calendar;
        public static DateTime DayAndMonth;

        public NewWeekPlanning()
        {
            InitializeComponent();
            SqliteHandler.LoadAllEmployeesOnName("");
            if (UcFunctions.SelectedFunction != "Bezorger" || UcDepots.SelectedDepot != "Bilthoven")
            {
                SqliteHandler.Bedrijven = null;
                CompanyFieldComboBox.Visibility = Visibility.Hidden;
                List<string> names = new List<string>();

                foreach (var item in SqliteHandler.LoadAllEmployeesOnName(""))
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
            else
            {
                CompanyFieldComboBox.ItemsSource = SqliteHandler.LoadAllCompanyNames();
            }
            FunctieNames();
            BeginTimeFieldComboBox.ItemsSource = FunctieBeginTijden();
            EndTimeFieldComboBox.ItemsSource = FunctieEindTijden();
        }

        private string[] FunctieBeginTijden()
        {
            switch (UcFunctions.SelectedFunction)
            {
                case "Bezorger":
                    string[] BezorgersTijden = { "10:00", "13:30", "17:30" };
                    return BezorgersTijden;
                case "DepotWerk":
                    string[] DepotWerkTijden = { "06:00", "07:00", "10:00", "13:00", "15:00" };
                    return DepotWerkTijden;
                case "SorteerWerk":
                    string[] SorteerWerkTijden = { "06:00", "07:00", "13:00" };
                    return SorteerWerkTijden;
                case "AllEmployees":
                    string[] AllEmployeesTijden = { "06:00", "07:00", "10:00", "13:00", "13:30", "15:00", "17:30" };
                    return AllEmployeesTijden;
                default:
                    return null;
            }
        }

        private string[] FunctieEindTijden()
        {
            switch (UcFunctions.SelectedFunction)
            {
                case "Bezorger":
                    string[] BezorgersTijden = { "15:00", "18:00", "21:30" };
                    return BezorgersTijden;
                case "DepotWerk":
                    string[] DepotWerkTijden = { "10:00", "15:00", "21:30" };
                    return DepotWerkTijden;
                case "SorteerWerk":
                    string[] SorteerWerkTijden = { "10:00", "15:30" };
                    return SorteerWerkTijden;
                case "AllEmployees":
                    string[] AllEmployeesTijden = { "10:00", "15:00", "15:30", "18:00", "21:30" };
                    return AllEmployeesTijden;
                default:
                    return null;
            }
        }

        private List<Functions> FunctieNames()
        {
            if (UcFunctions.SelectedFunction == "AllEmployees")
            {
                FunctieFieldComboBox.Visibility = Visibility.Visible;
                if (this.PersNrTextField.Text != String.Empty)
                {
                    int persNr = int.Parse(this.PersNrTextField.Text);
                    return SqliteHandler.GetFunctieBasedOnPersNr(persNr);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                FunctieFieldComboBox.Visibility = Visibility.Hidden;
                return null;
            }
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
                if (employees != null)
                {
                    employees.Clear();
                }
                if (AllEmployees != null)
                {
                    AllEmployees.Clear();
                }
                DayDataGrid.ItemsSource = null;
                DateTime date = calendar.SelectedDate.Value;
                DayAndMonth = calendar.SelectedDate.Value;
                this.DatumInputBox.Text = $"{date.Year}-{date.Month}-{date.Day}";
                year = calendar.SelectedDate.Value;
                if (UcFunctions.SelectedFunction == "AllEmployees")
                {
                    AllEmployees = await Task.Run(() => SqliteHandler.LoadEmployeesOnDate($"{date.Year}-{date.Month}-{date.Day}"));
                    DayDataGrid.ItemsSource = AllEmployees;
                }
                else
                {
                    employees = await Task.Run(() => SqliteHandler.LoadEmployeesOnDate($"{date.Year}-{date.Month}-{date.Day}"));
                    DayDataGrid.ItemsSource = employees;
                }
            }
        }

        private void PlacePersNrInField(object sender, SelectionChangedEventArgs e)
        {
            if (NameFieldComboBox.SelectedItem != null && LastNameFieldComboBox.SelectedItem != null)
            {
                var PersNr = SqliteHandler.LoadEmployeePersNr(NameFieldComboBox.SelectedItem.ToString(), LastNameFieldComboBox.SelectedValue.ToString());
                PersNrTextField.Text = PersNr[0];
                FunctieNames();
                List<string> employeeFunctions = new List<string>();
                if (FunctieNames().ToList() != null)
                {
                    if (EmployeeHasFunctie.Bezorger == 1)
                    {
                        employeeFunctions.Add("Bezorger");
                    }
                    if (EmployeeHasFunctie.Sorteerpersoneel == 2)
                    {
                        employeeFunctions.Add("Sorteerpersoneel");
                    }
                    if (EmployeeHasFunctie.Depotpersoneel == 3)
                    {
                        employeeFunctions.Add("Depotpersoneel");
                    }
                    FunctieFieldComboBox.ItemsSource = employeeFunctions;
                }
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
            if (NameFieldComboBox.SelectedItem != null && LastNameFieldComboBox.SelectedItem != null)
            {
                var PersNr = SqliteHandler.LoadEmployeePersNr(NameFieldComboBox.SelectedItem.ToString(), LastNameFieldComboBox.SelectedValue.ToString());
                PersNrTextField.Text = PersNr[0];
            }
        }

        private async void AddNewWorkDayBtn_Click(object sender, RoutedEventArgs e)
        {

            SaveNewPerson employee = new SaveNewPerson();

            employee.Datum = $"{year.Year}-{DayAndMonth.Month}-{DayAndMonth.Day}";
            employee.Begin_tijd = this.BeginTimeFieldComboBox.Text;
            employee.Eind_tijd = this.EndTimeFieldComboBox.Text;
            switch (UcDepots.SelectedDepot)
            {
                case "Bilthoven":
                    employee.Depot = 1;
                    break;
                case "Almere":
                    employee.Depot = 2;
                    break;
                case "Lelystad":
                    employee.Depot = 3;
                    break;
                default:
                    break;
            }
            switch (FunctieFieldComboBox.Text)
            {
                case "Bezorger":
                    employee.Functie = 1;
                    break;
                case "Sorteerpersoneel":
                    employee.Functie = 2;
                    break;
                case "Depotpersoneel":
                    employee.Functie = 3;
                    break;
                default:
                    break;
            }
            if (this.PersNrTextField.Text != String.Empty && employee.Begin_tijd != String.Empty)
            {
                employee.PersId = int.Parse(this.PersNrTextField.Text);
            }
            else
            {
                employee.PersId = -1;
            }
            if (employee.PersId != -1 && employee.Datum != String.Empty)
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

        private void PlaceNameInFieldBasesOnCompany(object sender, SelectionChangedEventArgs e)
        {
            List<string> names = new List<string>();

            foreach (var item in SqliteHandler.LoadAllEmployeesOnName(this.CompanyFieldComboBox.SelectedItem.ToString()))
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
    }
}
