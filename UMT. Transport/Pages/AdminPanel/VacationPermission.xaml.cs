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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UMT.Transport.Classes;

namespace UMT.Transport.Pages.AdminPanel
{
    /// <summary>
    /// Interaction logic for VacationPermission.xaml
    /// SOFTWARE CREATED BY Yeray Guzmán Padrón.
    /// GITHUB: https://github.com/yeray036
    /// </summary>
    public partial class VacationPermission : Page
    {
        private static Calendar calendar;
        List<int>selectedDayFromCalendar;
        List<int>selectedMonthFromCalendar;

        public VacationPermission()
        {
            InitializeComponent();
            SqliteHandler.GetEmployeeNamesAndPersNr();
            var employees = SqliteHandler.GetEmployeeNamesAndPersNr();
            foreach (var item in employees)
            {
                NameComboBox.Items.Add(item.Naam);
            }
            VacationDataGrid.ItemsSource = SqliteHandler.fullVacations().ToList();
        }

        private void InsertPersNrBasedOnName(object sender, SelectionChangedEventArgs e)
        {
            var selectedName = NameComboBox.SelectedItem.ToString();
            var splitFullname = selectedName.Split(' ');
            if (splitFullname.Length >= 3)
            {
                PersNrTextbox.Text = SqliteHandler.LoadEmployeePersNr(splitFullname[0], $"{splitFullname[1]} {splitFullname[2]}")[0];
            }
            else
            {
                PersNrTextbox.Text = SqliteHandler.LoadEmployeePersNr(splitFullname[0], $"{splitFullname[1]}")[0];
            }
        }

        private void GetFirstDateAndLastDateVacation_Calendar(object sender, SelectionChangedEventArgs e)
        {
            VacationBeginText.Text = "";
            VacationEndText.Text = "";
            calendar = sender as Calendar;
            if (calendar.SelectedDate.HasValue)
            {
                selectedDayFromCalendar = new List<int>();
                selectedMonthFromCalendar = new List<int>();
                foreach (var day in calendar.SelectedDates)
                {
                    selectedDayFromCalendar.Add(day.Day);
                    selectedMonthFromCalendar.Add(day.Month);
                }
                var selectedYear = calendar.SelectedDate.Value;
                VacationBeginText.Text = $"{selectedYear.Year}-{selectedMonthFromCalendar[0]}-{selectedDayFromCalendar[0]}";
                VacationEndText.Text = $"{selectedYear.Year}-{selectedMonthFromCalendar[selectedMonthFromCalendar.Count - 1]}-{selectedDayFromCalendar[selectedDayFromCalendar.Count - 1]}";
            }
        }

        private void AcceptVerlof_Btn(object sender, RoutedEventArgs e)
        {
            if (NameComboBox.Text != string.Empty && PersNrTextbox.Text != string.Empty)
            {
                EmployeeVacation employeeVacation = new EmployeeVacation();
                employeeVacation.PersNr = int.Parse(PersNrTextbox.Text);
                if (VacationBeginText.Text != string.Empty && VacationEndText.Text != string.Empty)
                {
                    employeeVacation.Verlof_start = VacationBeginText.Text;
                    employeeVacation.Verlof_eind = VacationEndText.Text;
                    SqliteHandler.InsertVacationIntoDb(employeeVacation);
                    VacationDataGrid.ItemsSource = SqliteHandler.fullVacations().ToList();
                }
                else
                {
                    MessageBox.Show("Geen verlof data ingevuld.");
                }
            }
            else
            {
                MessageBox.Show("Geen werknemer geselecteerd.");
            }
        }
    }
}
