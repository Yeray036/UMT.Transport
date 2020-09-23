using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RemoveWrongWorkday.xaml
    /// </summary>
    public partial class RemoveWrongWorkday : Page
    {
        public RemoveWrongWorkday()
        {
            InitializeComponent();
            SqliteHandler.GetEmployeeNamesAndPersNr();
            var employees = SqliteHandler.GetEmployeeNamesAndPersNr();
            foreach (var item in employees)
            {
                NameComboBox.Items.Add(item.Naam);
            }
        }

        private void InsertEmployeePersNrBasedOnName(object sender, SelectionChangedEventArgs e)
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
            CurrentEmployeeWorkdays.ItemsSource = null;
            CurrentEmployeeWorkdays.ItemsSource = SqliteHandler.GetAllWorkdaysFromEmployee(int.Parse(PersNrTextbox.Text));
        }

        private void GetSelectedWorkdays(object sender, SelectedCellsChangedEventArgs e)
        {
            if (CurrentEmployeeWorkdays.SelectedItems.Count > 0)
            {
                for (int i = 0; i < CurrentEmployeeWorkdays.SelectedItems.Count; i++)
                {
                    TextBlock DatumText = CurrentEmployeeWorkdays.Columns[0].GetCellContent(CurrentEmployeeWorkdays.Items[i]) as TextBlock;
                    TextBlock BeginTijdText = CurrentEmployeeWorkdays.Columns[1].GetCellContent(CurrentEmployeeWorkdays.Items[i]) as TextBlock;
                    if (SelectedWorkdaysListBox.Items.Contains(DatumText.Text + " >" + BeginTijdText.Text))
                    {
                        continue;
                    }
                    else
                    {
                        SelectedWorkdaysListBox.Items.Add(DatumText.Text + " >" + BeginTijdText.Text);
                    }
                }
            }
        }

        private void RemoveSelectedWorkdays(object sender, RoutedEventArgs e)
        {
            if (SelectedWorkdaysListBox.Items.Count > 0)
            {
                List<string> selectedWorkdaysList = new List<string>();
                List<string> selectedWorkdaysListTime = new List<string>();
                foreach (var item in SelectedWorkdaysListBox.Items)
                {
                    var onlyDate = item.ToString().Split('>');
                    string ConvertedDate = onlyDate[0].ToString().Replace('-', '/');
                    DateTime dt = Convert.ToDateTime(ConvertedDate);
                    if (selectedWorkdaysList.Contains($"{dt.Year}-{dt.Month}-{dt.Day}"))
                    {
                        continue;
                    }
                    else
                    {
                        selectedWorkdaysList.Add($"{dt.Year}-{dt.Month}-{dt.Day}");
                    }
                    if (selectedWorkdaysListTime.Contains($"{onlyDate[1]}"))
                    {
                        continue;
                    }
                    else
                    {
                        selectedWorkdaysListTime.Add(onlyDate[1].ToString());
                    }
                }
                if (PersNrTextbox.Text != string.Empty)
                {
                    SqliteHandler.RemoveSelectedWorkdays(selectedWorkdaysList, int.Parse(PersNrTextbox.Text), selectedWorkdaysListTime);
                    CurrentEmployeeWorkdays.ItemsSource = SqliteHandler.GetAllWorkdaysFromEmployee(int.Parse(PersNrTextbox.Text));
                    SelectedWorkdaysListBox.Items.Clear();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Geen werkdag(en) geselecteerd.");
            }
        }

        private void RemoveSelectedItemFromList(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                SelectedWorkdaysListBox.Items.Remove(SelectedWorkdaysListBox.SelectedItem);
            }
        }
    }
}
