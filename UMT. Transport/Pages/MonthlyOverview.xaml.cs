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

namespace UMT.Transport.Pages
{
    /// <summary>
    /// Interaction logic for MonthlyOverview.xaml
    /// </summary>
    public partial class MonthlyOverview : Page
    {
        public static System.Windows.Controls.Calendar calendar;
        List<PersonModel> employees = new List<PersonModel>();

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
                MonthlyOverviewDataGrid.ItemsSource = null;
                List<int> selectedDayFromWeek = new List<int>();
                List<int> selectedMonthFromWeek = new List<int>();
                foreach (var day in calendar.SelectedDates)
                {
                    selectedDayFromWeek.Add(day.Day);
                    selectedMonthFromWeek.Add(day.Month);
                }
                DateTime year = calendar.SelectedDate.Value;
                employees = await Task.Run(() => SqliteHandler.LoadEmployeesOnWeek($"{selectedDayFromWeek[0]}-{selectedMonthFromWeek[0]}", $"{selectedDayFromWeek[selectedDayFromWeek.Count - 1]}-{selectedMonthFromWeek[selectedMonthFromWeek.Count - 1]}", $"{year.Year}"));
                MonthlyOverviewDataGrid.ItemsSource = employees;
            }
        }
    }
}
