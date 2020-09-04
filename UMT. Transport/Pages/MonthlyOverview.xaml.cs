using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
using UMT.Transport.Classes;
using UMT.Transport.UserControls;

namespace UMT.Transport.Pages
{
    /// <summary>
    /// Interaction logic for MonthlyOverview.xaml
    /// </summary>
    public partial class MonthlyOverview : Page
    {
        public static System.Windows.Controls.Calendar calendar;
        List<PersonModel> employees = new List<PersonModel>();
        List<AllEmployeesPerDepot> AllEmployees = new List<AllEmployeesPerDepot>();
        List<int> selectedDayFromWeek;
        List<int> selectedMonthFromWeek;
        DateTime year;

        public MonthlyOverview()
        {
            InitializeComponent();
        }

        private async void ShowWeekWorkDays_calendar(object sender, SelectionChangedEventArgs e)
        {
            calendar = sender as System.Windows.Controls.Calendar;

            if (calendar.SelectedDate.HasValue)
            {
                await Task.Run(() => Dispatcher.BeginInvoke(
                    new ThreadStart(async () => await LoadDataGridFromWeekDates(calendar))));
            }
        }

        private async Task LoadDataGridFromWeekDates(System.Windows.Controls.Calendar calendar)
        {
            if (calendar.SelectedDates.Count > 1)
            {
                employees.Clear();
                AllEmployees.Clear();
                MonthlyOverviewDataGrid.ItemsSource = null;
                selectedDayFromWeek = new List<int>();
                selectedMonthFromWeek = new List<int>();
                foreach (var day in calendar.SelectedDates)
                {
                    selectedDayFromWeek.Add(day.Day);
                    selectedMonthFromWeek.Add(day.Month);
                }
                year = calendar.SelectedDate.Value;
                if (UcFunctions.SelectedFunction == "AllEmployees")
                {
                    AllEmployees = await Task.Run(() => SqliteHandler.LoadEmployeesOnWeek($"{selectedDayFromWeek[0]}-{selectedMonthFromWeek[0]}", $"{selectedDayFromWeek[selectedDayFromWeek.Count - 1]}-{selectedMonthFromWeek[selectedMonthFromWeek.Count - 1]}", $"{year.Year}"));
                    if (AllEmployees.Count <= 0)
                    {
                        MessageBox.Show("Geen geplande data gevonden");
                    }
                    else
                    {
                        MonthlyOverviewDataGrid.ItemsSource = AllEmployees;
                    }
                }
                else
                {
                    employees = await Task.Run(() => SqliteHandler.LoadEmployeesOnWeek($"{selectedDayFromWeek[0]}-{selectedMonthFromWeek[0]}", $"{selectedDayFromWeek[selectedDayFromWeek.Count - 1]}-{selectedMonthFromWeek[selectedMonthFromWeek.Count - 1]}", $"{year.Year}"));
                    if (employees.Count <= 0)
                    {
                        MessageBox.Show("Geen geplande data gevonden");
                    }
                    else
                    {
                        MonthlyOverviewDataGrid.ItemsSource = employees;
                    }
                }
            }
        }

        private void PrintDataGrid_Btn(object sender, RoutedEventArgs e)
        {
            if (MonthlyOverviewDataGrid.ItemsSource != null)
            {
                MessageBox.Show("Voor dat je gaat printen zorg ervoor dat je in landscape 'LIGGEND' print.");
                PrintDialog Printdlg = new PrintDialog();
                if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
                {
                    Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                    // sizing of the element.
                    MonthlyOverviewDataGrid.Measure(pageSize);
                    MonthlyOverviewDataGrid.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                    Printdlg.PrintVisual(MonthlyOverviewDataGrid, $"Planning van {selectedDayFromWeek[0]}-{selectedMonthFromWeek[0]} tot {selectedDayFromWeek[selectedDayFromWeek.Count - 1]}-{selectedMonthFromWeek[selectedMonthFromWeek.Count - 1]} {year.Year}");
                }
            }
            else
            {
                MessageBox.Show("Een lege pagina kan je niet printen!");
            }
        }
    }
}
