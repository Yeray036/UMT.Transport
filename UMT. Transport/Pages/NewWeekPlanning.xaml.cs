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

        public NewWeekPlanning()
        {
            InitializeComponent();
            NameFieldComboBox.ItemsSource = SqliteHandler.LoadAllEmployeesOnName();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;

            if (calendar.SelectedDate.HasValue)
            {
                employees.Clear();
                DayDataGrid.ItemsSource = null;
                DateTime date = calendar.SelectedDate.Value;
                this.DatumInputBox.Text = $"{date.Day}-{date.Month}";
                employees = SqliteHandler.LoadEmployeesOnDate($"{date.Day}-{date.Month}");
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
    }
}
