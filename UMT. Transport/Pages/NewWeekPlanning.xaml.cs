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
            /*if (UcFunctions.SelectedFunction != "AllEmployees")
            {
                FunctieFieldComboBox.Visibility = Visibility.Hidden;
            }
            else
            {
                FunctieFieldComboBox.Visibility = Visibility.Visible;
                FunctieFieldComboBox.ItemsSource = FunctieNames();

            }
            */
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
                CompanyFieldComboBox.ItemsSource = SqliteHandler.Bedrijven;
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
                employees.Clear();
                AllEmployees.Clear();
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
                List<string> employeeFunctions = new List<string>();
                FunctieNames();
                if (FunctieNames() != null)
                {
                    if (FunctieNames()[0].Bezorger != 0)
                    {
                        employeeFunctions.Add("Bezorger");
                    }
                    if (FunctieNames()[0].Depot_personeel != 0)
                    {
                        employeeFunctions.Add("Depot_personeel");
                    }
                    if (FunctieNames()[0].Sorteer_personeel != 0)
                    {
                        employeeFunctions.Add("Sorteer_personeel");
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
            if (UcFunctions.SelectedFunction == "AllEmployees")
            {
                AllEmployeesPerDepot allEmployees = new AllEmployeesPerDepot();

                allEmployees.Datum = $"{year.Year}-{DayAndMonth.Month}-{DayAndMonth.Day}";
                allEmployees.Begin_tijd = this.BeginTimeFieldComboBox.Text;
                allEmployees.Eind_tijd = this.EndTimeFieldComboBox.Text;
                if (this.PersNrTextField.Text != String.Empty && allEmployees.Begin_tijd != String.Empty)
                {
                    allEmployees.PersNr = this.PersNrTextField.Text;
                }
                else
                {
                    allEmployees.PersNr = "-1";
                }
                if (allEmployees.PersNr != "-1" && allEmployees.Datum != String.Empty)
                {
                    switch (FunctieFieldComboBox.SelectedItem)
                    {
                        case "Bezorger":
                            allEmployees.Bezorger = allEmployees.PersNr;
                            break;
                        case "Depot_personeel":
                            allEmployees.Depot_personeel = allEmployees.PersNr;
                            break;
                        case "Sorteer_personeel":
                            allEmployees.Sorteer_personeel = allEmployees.PersNr;
                            break;
                        default:
                            break;
                    }
                    if (allEmployees.Bezorger == "0" || allEmployees.Bezorger == string.Empty && allEmployees.Depot_personeel == "0" || allEmployees.Depot_personeel == string.Empty && allEmployees.Sorteer_personeel == "0" || allEmployees.Sorteer_personeel == string.Empty)
                    {
                        MessageBox.Show("Je bent de functie vergeten");
                        return;
                    }
                    else
                    {
                        SqliteHandler.SaveNewEmployeeWorkDay(null, allEmployees);
                        await Task.Run(() => Dispatcher.BeginInvoke(
                            new ThreadStart(async () => await LoadDataGridFromDate(calendar))));
                        this.NameFieldComboBox.Text = "";
                        this.LastNameFieldComboBox.Text = "";
                        this.PersNrTextField.Text = "";
                        this.BeginTimeFieldComboBox.Text = "";
                        this.EndTimeFieldComboBox.Text = "";
                        this.FunctieFieldComboBox.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Geen geldige werkdag data ingevoerd.");
                    return;
                }
            }
            else
            {
                PersonModel employee = new PersonModel();

                employee.Datum = $"{year.Year}-{DayAndMonth.Month}-{DayAndMonth.Day}";
                employee.Begin_tijd = this.BeginTimeFieldComboBox.Text;
                employee.Eind_tijd = this.EndTimeFieldComboBox.Text;
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
                    SqliteHandler.SaveNewEmployeeWorkDay(employee, null);
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
